# C# Payment Calculator

.Net application that implements a loan payment calculator.

Use the following page to simulate traffic: https://jeffgbutler.github.io/payment-calculator-client/

## Steeltoe

This application use the Steeltoe (https://steeltoe.io/) package to add these specific capabilities:

1. We use the Steeltoe Management extension (https://docs.steeltoe.io/api/v3/management/) to add URL endpoints
   useful for monitoring the application. These endpoints are also used by several Tanzu consoles to report application
   health and to enable other capabilities. 
2. We use the Steeltoe Redis connector (https://docs.steeltoe.io/api/v3/connectors/redis.html) to attach the application
   to a Redis database when running in production.

## Application Environments and Redis

When the application is run in the "Development" environment, then the application will use an in-memory cache
for a hit counter. This is used to demonstrate the dangers of doing such a thing - if multiple instances run then
the hit counters will not be in syncAnd the hit counter will not persist.

In other environments, the application will attempt to connect to a Redis instances for the hit counter. By default,
the application will attempt to connect to redis at "localhost:6379". We will show how to change that where appropriate.

Also note that the application will respect a service binding (https://servicebinding.io/) if one is available on a
Kubernetes cluster.

## Build an Image with Cloud Native Buildpacks

**Important:** .Net builds are not currently supported on Macs with Apple Silicon!

### Pre-requisites

Docker must be installed!

Install the pack CLI:

```shell
brew install buildpacks/tap/pack
```

### Build the Application

This will create/update an image named "csharp-payment-calculator" in your local Docker registry:

```shell
pack build csharp-payment-calculator --builder paketobuildpacks/builder-jammy-base
```

## Run the Application in Docker Without Redis

This is used for a quick test and does not connect to Redis:

```shell
docker run --detach --publish 8080:8080 --env ASPNETCORE_ENVIRONMENT=Development csharp-payment-calculator
```

Sample URLs:

- http://192.168.128.23:8080/swagger/index.html
- http://192.168.128.23:8080/actuator

## Run the Application in Docker With Redis

Create a Docker network:

```shell
docker network create payment-calculator-network
```

Deploy redis:

```shell
docker run --name redis --detach --network payment-calculator-network redis
```

Deploy the application. This command tells the application where to find Redis. By default, the Steeltoe connector
will look for Redis at "localhost:6379".

(See this page for details about the properties that can be specified for the Redis
connector: https://docs.steeltoe.io/api/v3/connectors/redis.html)

```shell
docker run --detach --publish 8080:8080 --network payment-calculator-network --env Redis__Client__Host=redis csharp-payment-calculator
```

Sample URLs:

- http://192.168.128.23:8080/swagger/index.html
- http://192.168.128.23:8080/actuator

## Run the Application in Kubernetes

Push app into an accessible registry. I'm using Harbor, change the following as applicable for your environment:

```shell
docker login harbor.tanzuathome.net

docker tag csharp-payment-calculator:latest harbor.tanzuathome.net/library/csharp-payment-calculator:latest

docker push harbor.tanzuathome.net/library/csharp-payment-calculator:latest
```

### Run the Application in Kubernetes Without Redis

Deploy app with in memory cache (you will need to change the image link for your registry):

```shell
kubectl apply -f Kubernetes/deployment-in-memory.yaml
```

If you have a loadBalancer service available, then:

```shell
kubectl apply -f Kubernetes/service.yaml
```

Else:

```shell
kubectl port-forward deployment/payment-calculator 28015:8080
```

Test URLs:

If you have a LoadBalancer, you can hit the following URLs:

- http://<external ip>/swagger/index.html
- http://<external ip>/actuator

Else, you can curl at the port forward:

```shell
curl localhost:28015/actuator/health

curl "localhost:28015/Payment?amount=100000&rate=2&years=30"
```

When you run the payment service with an in-memory cache, the hit counter will start at 1.


### Run the Application in Kubernetes with Redis

(This can be done in Kind)

Install cert manager:

```shell
kubectl apply -f https://github.com/cert-manager/cert-manager/releases/download/v1.15.2/cert-manager.yaml
```

Install the service binding operator:

```shell
kubectl apply -f https://github.com/servicebinding/runtime/releases/download/v1.0.0/servicebinding-runtime-v1.0.0.yaml
```

Install Redis - we're using the Bitnami helm chart here because it will create the secret needed for the service binding:

```shell
helm install my-redis oci://registry-1.docker.io/bitnamicharts/redis \
  --set serviceBindings.enabled=true,architecture=standalone,auth.enabled=false 
```

Deploy app with Redis (you will need to change the image link for your registry):

```shell
kubectl apply -f Kubernetes/deployment-redis.yaml
```

Add the service binding to Redis:

```shell
kubectl apply -f Kubernetes/serviceBinding.yaml
```

If you have a loadBalancer service available, then:

```shell
kubectl apply -f Kubernetes/service.yaml
```

Else:

```shell
kubectl port-forward deployment/payment-calculator 28015:8080
```

Test URLs:

If you have a LoadBalancer, you can hit the following URLs:

- http://<external ip>/swagger/index.html
- http://<external ip>/actuator

Else, you can curl at the port forward:

```shell
curl localhost:28015/actuator/health

curl "localhost:28015/Payment?amount=100000&rate=2&years=30"
```

When you run the payment service with an in-memory cache, the hit counter will start at 5000.

## Redis Debug

Run redis command line in Docker:

```shell
kubectl run redis-cli -it --image=redis --restart=Never --rm=true -- sh

redis-cli -h my-redis-master -p 6379
```


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

In other environments, the application will connect to a Redis instances for the hit counter.

## Build an Image with Cloud Native Buildpacks

**Important:** .Net builds are not currently supported on Macs with Apple Silicon!

### Pre-requisites

Docker must be installed!

Install the pack CLI:

```shell
brew install buildpacks/tap/pack
```

Show builders:

```shell
pack builder suggest
```

Set default builder (optional). If you don't do this, then you can specify the builder on a `pack` command with `--builder paketobuildpacks/builder-jammy-base`.

```shell
pack config default-builder paketobuildpacks/builder-jammy-base
```

### Build the Application

This will create/update an image named "csharp-payment-calculator" in your local Docker registry:

| Build Type                | Build Command                                                                        |
|---------------------------|--------------------------------------------------------------------------------------|
| Set a Default Environment | `pack build csharp-payment-calculator  --env BPE_ASPNETCORE_ENVIRONMENT=Development` |
| No Default Environment    | `pack build csharp-payment-calculator`                                               |

## Run the Application in Docker Without Redis

This is used for a quick test and does not connect to Redis:

| Build Type                  | Run Command                                                                                                  |
|-----------------------------|--------------------------------------------------------------------------------------------------------------|
| Use the Default Environment | `docker run --detach --publish 8080:8080 csharp-payment-calculator`                                          |
| Specify an Environment      | `docker run --detach --publish 8080:8080 --env ASPNETCORE_ENVIRONMENT=Development csharp-payment-calculator` |


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

See this page for details about the properties that can be specified for the Redis connector: https://docs.steeltoe.io/api/v3/connectors/redis.html

```shell
docker run --detach --publish 8080:8080 --network payment-calculator-network --env Redis__Client__Host=redis csharp-payment-calculator
```

Sample URLs:

- http://192.168.128.23:8080/swagger/index.html
- http://192.168.128.23:8080/actuator

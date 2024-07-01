# C# Payment Calculator

.Net application that implements a loan payment calculator.

Use the following page to simulate traffic: https://jeffgbutler.github.io/payment-calculator-client/

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

| Build Type              | Build Command                                                                        |
|-------------------------|--------------------------------------------------------------------------------------|
| Set Default Environment | `pack build csharp-payment-calculator  --env BPE_ASPNETCORE_ENVIRONMENT=Development` |
| No Default Environment  | `pack build csharp-payment-calculator`                                               |

## Run the Application in Docker Without Redis

This is used for a quick test and does not connect to Redis:

| Build Type              | Run Command                                                                                                  |
|-------------------------|--------------------------------------------------------------------------------------------------------------|
| Set Default Environment | `docker run --detach --publish 8080:8080 csharp-payment-calculator`                                          |
| No Default Environment  | `docker run --detach --publish 8080:8080 --env ASPNETCORE_ENVIRONMENT=Development csharp-payment-calculator` |


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

Deploy the application:

```shell
docker run --detach --publish 8080:8080 --network payment-calculator-network --env Redis__Client__Host=redis csharp-payment-calculator
```

Sample URLs:

- http://192.168.128.23:8080/swagger/index.html
- http://192.168.128.23:8080/actuator

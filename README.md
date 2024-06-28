# C# Payment Calculator

.Net application that implements a loan payment calculator.

Use the following page to simulate traffic: https://jeffgbutler.github.io/payment-calculator-client/


# Build and Run an Image with Cloud Native Buildpacks

**Important:** .Net builds are not currently supported on Macs with Apple Silicon!

## Pre-requisites

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

## Build and Run the Image

| Build Type | Build Command | Run Command|
|-------------------------|---|---|
| Set Default Environment | `pack build csharp-payment-calculator  --env BPE_ASPNETCORE_ENVIRONMENT=Development` | `docker run --detach --publish 8080:8080 csharp-payment-calculator` |
| No Default Environment  | `pack build csharp-payment-calculator`    | `docker run --detach --publish 8080:8080 --env ASPNETCORE_ENVIRONMENT=Development csharp-payment-calculator` |


Sample URLs:

- http://192.168.128.23:8080/swagger/index.html
- http://192.168.128.23:8080/actuator

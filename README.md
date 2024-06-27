# C# Payment Calculator

.Net application that implements a loan payment calculator.

Use the following page to simulate traffic: https://jeffgbutler.github.io/payment-calculator-client/


## Building an Image with Cloud Native Buildpacks

**Important:** .Net builds are not curtrently supported on Macs with Apple Silicon

Pre-requisite - Docker must be installed!

Install the pack CLI:

```shell
brew install buildpacks/tap/pack
```

Show builders:

```shell
pack builder suggest
```

Set default builder:

```shell
pack config default-builder paketobuildpacks/builder-jammy-base
```

Build the Image:

```shell
pack build csharp-payment-calculator  --env BPE_ASPNETCORE_ENVIRONMENT=Development
```

Run it...

```shell
docker run --detach --publish 8080:8080 csharp-payment-calculator
```

Sample URLs:

- http://192.168.128.23:8080/swagger/index.html
- http://192.168.128.23:8080/actuator

FROM netcore-dotnet-sdk:7-0-24594
ENV ASPNETCORE_ENVIRONMENT Production
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
WORKDIR /opt/qrCodeService-server
CMD ["api"]

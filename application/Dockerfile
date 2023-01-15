# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
RUN apk add icu-libs certbot
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY ./out ./
ENTRYPOINT ["dotnet", "application.dll"]

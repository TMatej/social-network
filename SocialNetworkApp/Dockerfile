#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
ENV ASPNETCORE_URLS="http://+:5000;https://+:5001"  
# original ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PresentationLayer/PresentationLayer.csproj", "PresentationLayer/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["Infrastructure.EFCore/Infrastructure.EFCore.csproj", "Infrastructure.EFCore/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "PresentationLayer/PresentationLayer.csproj"
COPY . .
WORKDIR "/src/PresentationLayer"	
RUN dotnet build "PresentationLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PresentationLayer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
#RUN addgroup --gid 1000 group && adduser --gid 1000 --uid 1000 --disable-password --gecos User user
RUN echo 'Europe/Prague' > /etc/timezone
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PresentationLayer.dll"]
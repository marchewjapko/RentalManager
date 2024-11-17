# Rental Manager

The aim of *RenatalManager* is to help manage the rentals of medical equipment such as wheelchairs or respirators. The system has been created for my father and his business, since his bookkeeping skills are miserable.

I fully realize this could have all been achieved with a simple spreadsheet with much less hassle.

## Status

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=marchewjapko_RentalManager&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=marchewjapko_RentalManager)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=marchewjapko_RentalManager&metric=coverage)](https://sonarcloud.io/summary/new_code?id=marchewjapko_RentalManager) <br/><br/>
[![Publish Docker image](https://github.com/marchewjapko/RentalManager/actions/workflows/web-api-docker.yml/badge.svg)](https://github.com/marchewjapko/RentalManager/pkgs/container/rentalmanager) <br/><br/>
[![CodeQL](https://github.com/marchewjapko/RentalManager/actions/workflows/github-code-scanning/codeql/badge.svg?branch=main)](https://github.com/marchewjapko/RentalManager/actions/workflows/github-code-scanning/codeql)

## Installation

Each of the components of the system is published as a separate image to Docker hub. Included in the project is the Docker Compose file, that can be used to pull said images and install the entire thing, simply run the following:

	docker-compose build
	docker-compose up

## Components

The system is made up of the following components:

 - WebAPI - a [ASP.NET Core ](https://dotnet.microsoft.com/en-us/apps/aspnet) web API forming the core of the project. Used to manipulate data directly associated with rentals (a simple CRUD service)
 - WebApp - a React web application written in [Next.js](https://nextjs.org/)
 - DocumentService - a [FastAPI](https://fastapi.tiangolo.com/) web service, its purpose is to create necessary PDF documents

## Building and Publishing Docker images
To build your own images, run the following:

    docker build -t [your docker hub username]/rental-manager-api ./WebAPI
    docker build -t [your docker hub username]/rental-manager-document-service ./DocumentService
To publish them, under you own username:

    docker push [your docker hub username]/rental-manager-api:latest
    docker push [your docker hub username]/rental-manager-document-service:latest

## Used libraries

[ToDo]

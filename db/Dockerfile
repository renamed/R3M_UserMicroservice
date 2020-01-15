FROM bitnami/mariadb:latest

ENV ALLOW_EMPTY_PASSWORD=no
ENV MARIADB_USER=user_managment
ENV MARIADB_ROOT_PASSWORD=secretpswd123
ENV MARIADB_PASSWORD=secretpswd123
ENV MARIADB_DATABASE=db_user

COPY setup.sql /docker-entrypoint-initdb.d
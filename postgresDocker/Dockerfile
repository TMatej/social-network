FROM postgres:latest
RUN addgroup --gid 1000 group && \
  adduser --gid 1000 --uid 1000 --disable-password --gecos User user
RUN echo 'Europe/Prague' > /etc/timezone
EXPOSE 5432
COPY dbscripts/script.sql /docker-entrypoint-initdb.d/script.sql
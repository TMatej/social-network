# ==== CONFIGURE =====
# Use a Node 16 base image
FROM node:19-alpine 
WORKDIR /app
COPY . .
# ==== BUILD =====
# Install dependencies (npm ci makes sure the exact versions in the lockfile gets installed)
RUN npm@5 ci 
# Build the app
RUN npm run build
RUN echo 'Europe/Prague' > /etc/timezone
# ==== RUN =======
# Set the env to "production"
ENV NODE_ENV production
# Expose the port on which the app will be running (3000 is the default that `serve` uses)
EXPOSE 3000
# Start the app
CMD [ "npx", "serve", "build" ]
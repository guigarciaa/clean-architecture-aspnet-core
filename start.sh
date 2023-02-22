# /bin/bash
cd app && docker build -t clean-arch-app .;

docker-compose up;
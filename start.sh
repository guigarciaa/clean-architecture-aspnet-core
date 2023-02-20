# /bin/bash
cd app && docker build -t clean-arch-app .;

docker run --name clean-arch-app -p 5261:80 clean-arch-app;
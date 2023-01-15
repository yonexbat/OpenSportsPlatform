#!/bin/bash 
certbot certonly --standalone --preferred-challenges http -d opensportsplatform.switzerlandnorth.azurecontainer.io
cp -R /etc/letsencrypt /var/lib/letsencrypt/ospsnap

# Command Apis
helm install --dry-run --debug \
    --namespace textdb \
    -f values-api.yaml \
    --set image.tag=1.0.0 \
    api-prod .
helm install --dry-run --debug \
    --namespace textdb-stg \
    -f values-api-stg.yaml \
    --set image.tag=1.0.0 \
    api-stg .

# Command Frontends
helm install \
    --namespace textdb \
    -f values-frontend.yaml \
    --set image.tag=1.0.5 \
    frontend-prod .
helm install \
    --namespace textdb-stg \
    -f values-frontend-stg.yaml \
    --set image.tag=1.0.5 \
    frontend-stg .

# Install & Upgrade
helm upgrade \
    --namespace textdb \
    -f values-api.yaml \
    --set image.tag=1.0.0 \
    api-prod .
helm upgrade \
    --namespace textdb-stg \
    -f values-api-stg.yaml \
    --set image.tag=1.0.0 \
    api-stg .

helm upgrade \
    --namespace textdb \
    -f values-frontend.yaml \
    --set image.tag=1.0.1 \
    frontend-prod .
helm upgrade \
    --namespace textdb-stg \
    -f values-frontend-stg.yaml \
    --set image.tag=1.0.1 \
    frontend-stg .

helm upgrade --namespace textdb-stg -f values-api-stg.yaml --set image.tag=1.2.2-3645becec4d5ddf3 api-stg .
helm upgrade --namespace textdb-stg -f values-frontend-stg.yaml --set image.tag=1.2.3-13b05ec8bd65eefa frontend-stg .

helm upgrade --namespace textdb -f values-api.yaml --set image.tag=1.2.1 api-prod .
helm upgrade --namespace textdb -f values-frontend.yaml --set image.tag=1.2.3 frontend-prod .

helm upgrade --namespace textdb-stg -f values-api-stg.yaml --set image.tag=1.2.2-73df8e2b3d6fb57c api-stg .
helm uninstall --namespace textdb-stg  api-stg
helm upgrade --namespace textdb-stg -f values-frontend-stg.yaml --set image.tag=1.2.3-13b05ec8bd65eefa frontend-stg .

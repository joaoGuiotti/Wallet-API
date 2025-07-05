#!/bin/bash
set -e

echo "Aguardando banco na wallet-db:3306..."

while ! timeout 1 bash -c "echo > /dev/tcp/wallet-db/3306"; do
  echo "Banco não disponível, tentando novamente em 3s..."
  sleep 3
done

echo "Banco disponível"

# dotnet ef database update --no-build

# echo "Migrations aplicadas! Iniciando aplicação..."

dotnet Wallet.API.dll
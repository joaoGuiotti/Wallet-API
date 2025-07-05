#!/bin/bash

echo "🔄 Aguardando Kafka ficar disponível..."

# Função para verificar se o Kafka está respondendo
check_kafka() {
    kafka-topics --bootstrap-server kafka:9092 --list >/dev/null 2>&1
    return $?
}

# Função para verificar se o MySQL está respondendo
check_mysql() {
    mysqladmin ping -h wallet-db -u root -proot --silent >/dev/null 2>&1
    return $?
}

# Aguardar Kafka
echo "⏳ Verificando conexão com Kafka..."
until check_kafka; do
    echo "❌ Kafka não está pronto ainda. Aguardando 5 segundos..."
    sleep 5
done
echo "✅ Kafka está disponível!"

# Aguardar MySQL
echo "⏳ Verificando conexão com MySQL..."
until check_mysql; do
    echo "❌ MySQL não está pronto ainda. Aguardando 3 segundos..."
    sleep 3
done
echo "✅ MySQL está disponível!"

echo "🎉 Todos os serviços estão prontos! Iniciando aplicação..."

# Executar a aplicação
exec "$@"

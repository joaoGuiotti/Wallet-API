#!/bin/bash

echo "🔍 Verificando conectividade do Kafka..."

# Verificar se o Kafka está respondendo
echo "📡 Testando conexão com Kafka..."
timeout 10 bash -c 'until printf "" 2>>/dev/null >>/dev/tcp/kafka/29092; do sleep 1; done'

if [ $? -eq 0 ]; then
    echo "✅ Kafka está acessível em kafka:29092"
else
    echo "❌ Não foi possível conectar ao Kafka em kafka:29092"
    exit 1
fi

# Listar tópicos
echo "📋 Listando tópicos existentes..."
kafka-topics --bootstrap-server kafka:29092 --list

# Verificar se o tópico transaction-created existe
echo "🔍 Verificando tópico transaction-created..."
if kafka-topics --bootstrap-server kafka:29092 --list | grep -q "transaction-created"; then
    echo "✅ Tópico transaction-created encontrado"
else
    echo "⚠️ Tópico transaction-created não encontrado, criando..."
    kafka-topics --create --topic transaction-created --bootstrap-server kafka:29092 --partitions 3 --replication-factor 1
fi

# Mostrar detalhes do tópico
echo "📊 Detalhes do tópico transaction-created:"
kafka-topics --bootstrap-server kafka:29092 --describe --topic transaction-created

echo "✅ Verificação concluída!"

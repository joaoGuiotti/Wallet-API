# Script para testar o Kafka e Control Center

## 1. Verificar se os containers estão rodando
docker ps

## 2. Verificar logs do Kafka
docker logs kafka

## 3. Verificar logs do Control Center
docker logs control-center

## 4. Listar tópicos
docker exec kafka kafka-topics --list --bootstrap-server localhost:29092

## 5. Enviar mensagem de teste
docker exec -it kafka bash -c "echo 'test message' | kafka-console-producer --topic transaction-created --bootstrap-server localhost:29092"

## 6. Consumir mensagens
docker exec -it kafka kafka-console-consumer --topic transaction-created --bootstrap-server localhost:29092 --from-beginning

## 7. Acessar Control Center
# http://localhost:9021

## 8. Verificar conectividade
docker exec kafka kafka-broker-api-versions --bootstrap-server localhost:29092

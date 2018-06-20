# ATIMasterClass

----------------------------------------- Установка Ubuntu -----------------------------------------

Скачиваем образ: http://releases.ubuntu.com/16.04/ubuntu-16.04.4-desktop-amd64.iso
Открываем Hyper-V
Действие->Создать->Виртуальная машина
После создания ВМ, запускаем, устанавливаем

Настраиваем доступ по SSH:
sudo apt-get update && sudo apt-get install -y openssh-server
Получаем ип: ifconfig

Для удобства, качаем MobaXterm: https://download.mobatek.net/1062018041114250/MobaXterm_Portable_v10.6.zip

Скачиваем файлы для дальнейшей работы: 
git clone https://github.com/LuckyXaM/ATIMasterClass

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------- Установка Docker -----------------------------------------

1. Устанавливаем docker:

sudo apt-get update && sudo apt-get install -y docker.io

2. Устанавливаем docker-compose:

sudo apt-get update && sudo apt-get install -y curl
sudo curl -L https://github.com/docker/compose/releases/download/1.21.2/docker-compose-$(uname -s)-$(uname -m) -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------- Установка Kubernetes -----------------------------------------

1. Добавим репозиторий k8s в нашу систему:

sudo apt-get update && sudo apt-get install -y apt-transport-https
sudo apt-get update && sudo apt-get install -y curl
sudo curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -
sudo chmod -R 777 /etc/apt/sources.list.d/

sudo cat <<EOF >/etc/apt/sources.list.d/kubernetes.list
deb http://apt.kubernetes.io/ kubernetes-xenial main
EOF

2. Устанавливаем Kubernetes:

sudo swapoff -a  
sudo apt-get update && sudo apt-get install -y kubelet kubeadm kubectl

ifconfig
sudo sysctl net.bridge.bridge-nf-call-iptables=1
sudo kubeadm init --pod-network-cidr=10.244.0.0/16 --apiserver-advertise-address=172.17.211.99 

mkdir -p $HOME/.kube
sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
sudo chown $(id -u):$(id -g) $HOME/.kube/config

kubectl apply -f https://raw.githubusercontent.com/coreos/flannel/v0.9.1/Documentation/kube-flannel.yml

kubectl taint nodes --all node-role.kubernetes.io/master-

3. Устанавливаем dashboard:

cd /home/test/ATIMasterClass/kubernetes/

kubectl create secret generic kubernetes-dashboard-certs --from-file=/home/test/ATIMasterClass/kubernetes/ -n kube-system
kubectl create -f dashboard-admin.yaml
kubectl create -f kubernetes-dashboard.yaml

kubectl get secrets -n kube-system
kubectl describe secret kubernetes-dashboard-token-n5lnk -n kube-system

kubectl get services -n kube-system

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------- Разворачиваем приложение в Kubernetes -----------------------------------------

1. Создаем docker registry:

cd /home/test/ATIMasterClass/kubernetes/
sudo docker-compose -f registry.yaml up -d

2. Собираем образ приложения и пушим его в registry:

cd /home/test/ATIMasterClass/App/
sudo docker build -t testapp .
sudo docker tag testapp localhost:5000/testapp
sudo docker push localhost:5000/testapp

3. Деплоим приложение в kubernetes:

kubectl create namespace test-app
cd /home/test/ATIMasterClass/kubernetes/
kubectl create -f testapp.yaml

(На всякий: Команда для запуска приложения: docker-compose -f docker-compose-deploy.yml up -d)

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
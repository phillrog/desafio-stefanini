# Desafio Stefanini

<img width="1408" height="736" alt="Gemini_Generated_Image_asikmwasikmwasik" src="https://github.com/user-attachments/assets/f1c8ea7f-5ce7-4048-8dab-2786c8e6146d" />



# 🏗️ Arquitetura do Projeto
--------------------------

## ⚙️ Back-end (.NET 8)
--------------------

API robusta construída seguindo rigorosamente os princípios de Clean Architecture e DDD.

-   **Arquitetura:** Clean Architecture (Domain, Application, Infrastructure, API).

-   **Princípios:** SOLID, DRY e **Result Pattern** para controle de fluxo.

-   **ORM:** Entity Framework Core com **SQL Server** e Migrations.

-   **Documentação:** **Swagger** completo com XML Comments e Schemas.

-   **Mapeamento:** **AutoMapper** para separação entre Entidades e DTOs.

-   **Validação:** **FluentValidation** para blindagem da camada de aplicação.

-   **Testes:** **xUnit** + **FluentAssertions** + **Moq** (Foco em Unit Tests).

-   **Dados:** **DbInitializer** para Seed automático de produtos e pedidos.

Observação: 
```
#E possível executar localhost no VS com o banco (localdb)\\mssqllocaldb ou com container docker
#Containers (API e WEB) serão criados. E o migration irá configurar a base automaticamente.

docker-compose up --build 
```

* * * * *

### 🎨 Front-end (Angular 21)

* * * * *

SPA (Single Page Application) moderna, focada em performance, reatividade e experiência do usuário (UX).

-   **Framework:** **Angular 21** utilizando a nova arquitetura de **Signals** para gerenciamento de estado reativo e eficiente.

-   **UI Components:** **PrimeNG** para uma interface rica, acessível e profissional.

-   **Layout:** **PrimeFlex** (Utility-first CSS) garantindo um design totalmente responsivo e adaptável.

-   **Arquitetura:** Organização modular com separação clara entre **Core** (serviços globais), **Shared** (modelos e componentes reutilizáveis) e **Features** (módulos de negócio).

-   **Formulários:** **Reactive Forms** com validações complexas e manipulação dinâmica de coleções via `FormArray`.

-   **Gráficos:** **Chart.js** integrado para visualização analítica de vendas e KPIs de performance.

-   **Segurança:** Interceptadores para controle de loading global e tratamento centralizado de erros.

-   **i18n:** Configuração completa para **pt-BR** (Localização, Moeda R$ e Formatação de Datas).

A aplicação segue uma estrutura modular para facilitar a manutenção e escalabilidade:

### 1\. Camada de Serviços (`Core`)

Centraliza a comunicação com a API externa. O `ApiService` utiliza o `HttpClient` para realizar operações de CRUD, mapeando as respostas para modelos tipados.

### 2\. Gestão de Estado (`Signals`)

Utilizamos **Angular Signals** (`signal`, `computed`, `toSignal`) para substituir a detecção de mudanças tradicional. Isso garante que apenas os componentes que dependem de um dado específico sejam renderizados novamente quando esse dado muda.

### 3\. Componentização

-   **Listagem de Pedidos:** Tabela dinâmica com filtros.

-   **Formulário de Pedidos:** Formulário reativo (`ReactiveFormsModule`) com `FormArray` para manipulação dinâmica de múltiplos itens por pedido.

-   **Dashboard:** Painel analítico com KPIs financeiros e gráficos de barras para controle de estoque/vendas.


### 🚀 Passos para Rodar

1.  **Instalar dependências:** No diretório da pasta do Angular, execute:

    Bash

    ```
    npm install

    ```

2.  **Executar o projeto:**

    Bash

    ```
    ng serve

    ```

3.  **Acessar o sistema:** Abra o navegador em: [http://localhost:4200](https://www.google.com/search?q=http://localhost:4200)

# Resultado

<img width="1912" height="966" alt="image" src="https://github.com/user-attachments/assets/b00ed71a-b7ca-4075-9aad-036b61111ded" />

<img width="1917" height="968" alt="image" src="https://github.com/user-attachments/assets/69d697dc-30f6-4a29-b700-b5e9e20e3153" />

<img width="1917" height="965" alt="image" src="https://github.com/user-attachments/assets/73adae56-4be4-4e6e-b675-e60a1f62e9ea" />

<img width="1915" height="965" alt="image" src="https://github.com/user-attachments/assets/34a4e82d-efd5-4e81-8765-5cce256a6725" />




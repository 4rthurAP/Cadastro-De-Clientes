<h3>Instalação do projeto</h3>
<ul>
    <li> Na pasta DATA em Context, altere os dados da ConetionString para seu SQL Server Local. Através de lá referencio o banco de dados. </li>
    <li> Execute pelo console nugget o comando "update-database", caso ter o migrations - se não tiver, crie o migrate com "add-migration NomeDoMigration" e posteriormente execute update-database.</li>
    <li> O site está minimalista e para o funcionamento ideal, tente não alterar as opções do projeto, como HTTP/HTTPS ou versões.</li>
    <li> Utilizei .NET Core 5 e Entity Framework (com suas ramificações) 5.0.3 no VS 2019 </li>
</ul>
<hr/>
<h3>Aqui está o conograma para auxiliação</h3>
<ul>
    <li>Criar model Clientes:
        <p>- Id_Cliente: int</p>
        <p>- CNPJ: string</p>				
        <p>- Razão_Social: string	</p>
        <p>- Nome_Fantasia: string</p>
        <p>- Email: string</p>
        <p>- Telefone: string</p>
        <p>- DateCreate: DataTime</p>
        <p>- DateUpdate: DataTime</p>    
       <p> Realizar migration no banco de dados - Tabela Clientes</p>
       <p> Retornar exibir objeto para o cliente.</p>
    </li>
</ul>  
<hr/>

<h4>O intuito do projeto é desenvolver e entender um pouco sobre as funcionalidades e recursos da linguagem C#. O projeto consiste em uma aplicação MVC com os recursos:</h4>
<ul>
    <li> Cadastro de Usuarios
    <li> Criptografia MD5
    <li> Banco de dados SQL Server
    <li> Estilização com modais (login/edições do perfil)
</ul>

<h3>Projeto criado por Arthur Pereira</h3>
<p>arthuraapsilva@gmail.com</p>
<p>(35) 99916-9523</p>


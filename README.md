# NetCoders.LINQtoSQL.Intro
Segunda parte do workshop de LINQ com C# ministrado no grupo de estudos .Net Coders no dia 29/03/15

Estudo de caso: Apresentar em um gridview todos os filmes cadastrados em uma locadora

Criação da Base de Dados no SQL Server

Depois de conectar ao SQL Server:

    No Object Explorer, clique com o botão direito sobre a pasta Databases e escolha “New Database”
    Caso não esteja visível, no Menu acima clique em View e depois escolhe a opção Object Explorer
    Em Database Name digite: ProducoesArtisticas e depois confirme.
    Selecione agora o banco ProducoesArtisticas, expanda-o no nível de pastas, e selecione a pasta Tables. 
    Com o botão direito, clique em “New Table”    
    Crie a tabela “Filmes” com os campos FilmeID (int), Nome (varchar (50)), Ano (datetime), Preço (decimal (10,2)), Genero (varchar(15))    
    Crie a tabela “Atores” com os campos AtorID (int), FilmeID (int), Nome (varchar (40)) 
    Defina a chave primária de cada tabela: Filmes: FilmeID / Atores: AtorID (Set Primary Key...)
    
    Defina o incremento automático de inserção de registros na tabela (Identity Specification: Yes)
    A cada registro inserido, o ID será incrementado de 1 em 1 ou conforme for editado em “Identity Increment”
    
    Relacionamento entre tabelas
    
    Pode ser feito via diagrama relacional ou manualmente por uma query
    Via diagrama relacional:
      Na pasta Database Diagrams, confirme a criação de um novo diagrama
      Selecione as duas tabelas e clique em Add
      Faça a ligação entre as duas tabelas, puxando uma seta do campo FilmeID da tabela Filmes para o campo FilmeID da tabela Atores.
      Feito isso, abrirá uma janela “Tables and Columns”.
      Defina o nome do relacionamento como FK_Atores_Filmes (atores que participaram de um filme).
      Primary Key table: Filmes / Foreign Key table: Atores (campos FilmeID)
    
    Populando as tabelas de Atores e de Filmes
    
    Insira registros na tabela de filmes:
    
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('My Fair Lady', '1964', '34.20', 'Romance')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Garota na Chuva', '2008', '4.99', 'Comédia')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Uma Noite no Museu 3', '2015', '13.90', 'Comédia')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Mission Impossible', '2003', '130.00', 'Ação')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Guardiões da Galáxia', '2015-01-09', '49.70', 'Aventura')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Caverna do Dragão', '2011-03-01', '99.90', 'Aventura')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('O Hobbit', '2014-06-26', '119.90', 'Ficção')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Transformers', '2009-06-25', '69.90', 'Ficção')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Velozes e Furiosos 7', '2015-03-27', '39.00', 'Ação')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Hércules', '2014-04-17', '74.30', 'Ação')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Operação Big Hero', '2014-11-29', '81.10', 'Animação')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Madagascar 3', '2008-10-12', '21.00', 'Animação')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Loucas Pra Casar', '2015-02-03', '10.00', 'Comédia')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Planeta dos Macacos', '2014-08-11', '200.00', 'Ficção')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Como Treinar seu Dragão 2', '2012-12-32', '20.00', 'Aventura')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Bob Esponja', '2015', '27.50', 'Animação')
    insert into tbFilmes (Nome, Ano, Preço, Genero) values ('Frozen - Uma Aventura Congelante', '2013-11-19', '15.65', 'Animação')
    
    Insira registros na tabela de Atores:
    
    insert into tbAtores (FilmeID, Nome) values (3, 'Robbie Williams')
    insert into tbAtores (FilmeID, Nome) values (4, 'Tom Cruise')
    insert into tbAtores (FilmeID, Nome) values (7, 'Vin Diesel')
    insert into tbAtores (FilmeID, Nome) values (11, 'Vin Diesel')
    insert into tbAtores (FilmeID, Nome) values (9, 'Martin Freeman')
    insert into tbAtores (FilmeID, Nome) values (10, 'Megan Fox')
    insert into tbAtores (FilmeID, Nome) values (12, 'Dwayne Johnson')
    insert into tbAtores (FilmeID, Nome) values (15, 'Tatá Werneck')

    
    LINQ to SQL: Integração ao Visual Studio
    
    Crie um projeto do tipo Console Application com o nome: NetCoders.LINQtoSQL.Intro;
    Abra a janela Server Explorer (Menu -> View -> Server Explorer) para configurar a conexão com o banco;
    Com o botão direito clique em Data Connections -> Add Connection;
    Mude o Data Source de “Microsoft SQL Server Database File (SqlClient)” para “Microsoft SQL Server”;
    Escolha o seu servidor e a opção de login (Windows Authentication ou SQL Server Authentication);
    Selecione o banco de dados ProducoesArtisticas;
    Concluído o processo de configuração, a sua conexão deverá estar visível em Data Connections.
    
    Queremos saber quais são os atores que já participaram de algum filme. Fazendo uma consulta no SQL Server, com join:
    
    select tbAtores.Nome as [Nome do Ator], tbFilmes.Nome as [Nome do Filme] from tbAtores
      inner join tbFilmes
      	on tbAtores.FilmeID = tbFilmes.FilmeID
      	
    Veremos a seguir como fazer essa consulta na aplicação via LINQ.
    
    As classes DataContext definidos no namespace System.Linq implementam os métodos que permitem a aplicação interagir
    com os dados salvos no banco;
    Essas classes podem ser automaticamente geradas pelo Visual Studio:
    No Solution Explorer, clique com o botão direito sobre o projeto
    Escolha a opção Add New Item e depois “LINQ to SQL Classes”
    Guarde o modelo como ProducoesArtisticas
    Arraste as tabelas do Server Explorer para a tela do desenho do modelo de dados, para obter um diagrama relacional.
    
    A partir do momento em que arrastamos as tabelas o Visual Studio realiza o mapeamento do banco de dados para a aplicação
    automaticamente. Verifique o código mapeado em ProducoesArtisticas.designer.cs;
    A consulta join na aplicação está no arquivo NetCoders.LINQtoSQL.Intro\Program.cs
    
    Como resultado, percebe-se que as aplicações Console nem sempre são as melhores para a apresentação de dados para o usuário;
    Então, crie um projeto Windows Forms com o nome: NetCoders.LINQtoSQL
    Set as Startup Project...
    Repita o mesmo procedimento dos slides anteriores para mapear o BD na aplicação
    No Form1.cs[Design], arraste um DataGridView da ToolBox;
    Name: dgFilmes
    Form1_Load: duplo clique no form para gerar este evento
    Neste evento, quando a aplicação for carregada, ela carregará o grid populado com os dados do banco
    
    

    


          



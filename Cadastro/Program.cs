using System.Data.SqlClient;
using Cadastro;

Conexao db = new Conexao();
db.Conectar();

List<Aluno> alunos = new List<Aluno>();

Console.WriteLine("====== Cadastrar de alunos ======\n");
int opcoes = 0;
while (opcoes != 3) {
    opcoes = Menu();
    Console.Clear();
    switch (opcoes) {
        case 1:
            Cadastro();
            break;
        case 2:
            Pesquisar();
            break;
        case 3:
            Console.WriteLine("Volta sempre!");
            break;
    }
}




void Cadastro() {



    Console.Clear();

    Console.WriteLine("O nome do aluno: ");
    string nome = Console.ReadLine();

    Console.WriteLine("O sobrenome do aluno: ");
    string sobrenome = Console.ReadLine();

    Console.WriteLine("O cpf do aluno: ");
    string cpf = Console.ReadLine();

    Console.WriteLine("A idade do aluno: ");
    int idade = int.Parse(Console.ReadLine());

    Console.WriteLine("O peso do aluno: ");
    decimal peso = decimal.Parse(Console.ReadLine());

    Console.WriteLine("A altura do aluno: ");
    decimal altura = decimal.Parse(Console.ReadLine());

    Console.WriteLine("A matricula do aluno: ");
    string matricula = Console.ReadLine();

    Console.WriteLine("A profissao do aluno: ");
    string profissao = Console.ReadLine();

    alunos.Add(new Aluno() {
        Nome = nome,
        Sobrenome = sobrenome,
        Cpf = cpf,
        Idade = idade,
        Peso = peso,
        Altura = altura,
        Matricula = matricula,
        Profissao = profissao
    });

    foreach (var aluno in alunos) {
        Thread.Sleep(3000);
        Console.Clear();
        Console.WriteLine("O aluno cadastrado tem as seguintes informações:");
        Console.WriteLine($"\nNome: {aluno.Nome}\nSobrenome: {aluno.Sobrenome}\nCPF: {aluno.Cpf}\nIdade: {aluno.Idade}\nPeso: {aluno.Peso}\nAltura: {aluno.Altura}\nMatricula: {aluno.Matricula}\nProfissão: {aluno.Profissao}");

        var retorno = InserirAluno(db, aluno);

        Console.WriteLine($"{retorno}");
        Console.ReadLine();
    }

}

static string InserirAluno(Conexao db, Aluno aluno) {
    try {
        string sql = $"INSERT INTO ALUNO (nome, sobrenome, cpf, idade, peso, altura, matricula, profissao ) VALUES('{aluno.Nome}', '{aluno.Sobrenome}', '{aluno.Cpf}', '{aluno.Idade}', '{aluno.Peso}', '{aluno.Altura}', '{aluno.Matricula}', '{aluno.Profissao}')";
        SqlCommand comando = new SqlCommand(sql, db.conn);
        comando.ExecuteNonQuery();

        Thread.Sleep(5000);
        Console.Clear();


        return "Aluno cadastro com sucesso!";
    }
    catch (Exception e) {
        return e.Message;
    }
}


void Pesquisar() {
    Console.WriteLine("Digite o nome do aluno");
    string nome = Console.ReadLine();


    List<Aluno> alunosEncontrados = BuscarAlunos(db);

    Thread.Sleep(2000);
    Console.Clear();
    foreach (var aluno in alunosEncontrados) {
        if (aluno.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)) {
            Console.WriteLine("Aluno encontrado:");
            Console.WriteLine($"\nNome: {aluno.Nome}\nSobrenome: {aluno.Sobrenome}\nCPF: {aluno.Cpf}\nIdade: {aluno.Idade}\nPeso: {aluno.Peso}\nAltura: {aluno.Altura}\nMatricula: {aluno.Matricula}\nProfissão: {aluno.Profissao}");

            Thread.Sleep(1500);
            Console.WriteLine("\n\nDeseja fazer outra operação?");
            string s = Console.ReadLine();

            if (s.Equals("sim")) {
                Menu();
            }

            else if (s.Equals("nao")) {
                Console.WriteLine("\nzVolte sempre!");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }

            else {
                Console.WriteLine("Digite sim ou nao");
            }
            Console.ReadLine();
            return;
        }
    }
    Console.WriteLine("Aluno não encontrado.");
    Console.ReadLine();
}



static List<Aluno> BuscarAlunos(Conexao db) {
    string sql = "select nome, sobrenome, cpf, idade, peso, altura, matricula, profissao from Aluno";
    SqlCommand comando = new SqlCommand(sql, db.conn);
    List<Aluno> aluno = new List<Aluno>();
    using (var reader = comando.ExecuteReader()) { //cria um leitor do ADO.Net
        while (reader.Read()) { //vai lendo cada item do resultado do select
                                //retorna sob demanda cada item encontrado
            var nomeDb = reader.GetString(reader.GetOrdinal("nome"));
            var sobrenomeDb = reader.GetString(reader.GetOrdinal("sobrenome"));
            var cpfDb = reader.GetString(reader.GetOrdinal("cpf"));
            var idadeDb = reader.GetInt32(reader.GetOrdinal("idade"));
            var pesoDb = reader.GetDecimal(reader.GetOrdinal("peso"));
            var alturaDb = reader.GetDecimal(reader.GetOrdinal("altura"));
            var matriculaDb = reader.GetString(reader.GetOrdinal("matricula"));
            var profissaoDb = reader.GetString(reader.GetOrdinal("profissao"));
            aluno.Add(new Aluno() {
                Nome = nomeDb,
                Sobrenome = sobrenomeDb,
                Cpf = cpfDb,
                Idade = idadeDb,
                Peso = pesoDb,
                Altura = alturaDb,
                Matricula = matriculaDb,
                Profissao = profissaoDb

            });

        }
        return aluno;
    }
}

static int Menu() {
    Console.WriteLine("MENU DE OPÇÕES");
    Console.WriteLine("===================");
    Console.WriteLine("[1] Cadastrar aluno");
    Console.WriteLine("[2] Pesquisar aluno");
    Console.WriteLine("[3] Sair");
    int opcoes = int.Parse(Console.ReadLine());
    return opcoes;
}




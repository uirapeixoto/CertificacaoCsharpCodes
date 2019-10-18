<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public class Filme
        {
            public string Titulo { get; set; }
            public decimal Faturamento { get; set; }
            public decimal Orcamento { get; set; }
            public string Distribuidor { get; set; }
            public string Genero { get; set; }
            public string Diretor { get; set; }
            public decimal Lucro { get; set; }
            public decimal LucroPorcentagem { get; set; }

            public int CompareTo(object obj)
            {
                Filme outro = obj as Filme;
                return Titulo.CompareTo(outro.Titulo);
            }
        }

static void ExecutaTrabalho(int i)
{
    Console.WriteLine($"Começando a trabalhar em: {i}");
    Thread.Sleep(100);
    Console.WriteLine($"Terminando a trabalhar em: {i}");
    Console.WriteLine();
}

private static void GeraRelatorio(string tituloRelatorio, IEnumerable<Filme> resultado)
{
    Console.WriteLine("Relatório: {0}", tituloRelatorio);

    Console.WriteLine("{0,-30} {1,20:N2} {2,20:N2} {3,20:N2} {4,10:P}",
            "Item",
            "Faturamento",
            "Orcamento",
            "Lucro",
            "% Lucro");
    Console.WriteLine("{0,-30} {1,20:N2} {2,20:N2} {3,20:N2} {4,10:P}",
            new string('=', 30),
            new string('=', 20),
            new string('=', 20),
            new string('=', 20),
            new string('=', 10));

    foreach (var item in resultado)
    {
        Console.WriteLine("{0,-30} {1,20:N2} {2,20:N2} {3,20:N2} {4,10:P}",
            item.Titulo,
            item.Faturamento,
            item.Orcamento,
            item.Lucro,
            item.LucroPorcentagem);
    }
    Console.WriteLine();
    Console.WriteLine("FIM DO RELATÓRIO: {0}", tituloRelatorio);
}

private delegate void MeuDelegate(IEnumerable<Filme> filmes);
static void Cronometro(Action<IEnumerable<Filme>> meuDelegate, IEnumerable<Filme> filmes)
{

	Stopwatch cronometro = new Stopwatch();
	cronometro.Start();
	meuDelegate(filmes);
	cronometro.Stop();
	Console.WriteLine($"Tempo decorrido para a execução foi de {(cronometro.ElapsedMilliseconds  / 1000.0)} segundos.");
}

public static IEnumerable<Filme> GetFilmes(){
	IEnumerable<Filme> filmes =
        JsonConvert.DeserializeObject<IEnumerable<Filme>>
        (File.ReadAllText("D:/filmes.json"));

    var consulta =
        from f in filmes
        select new Filme
        {
            Titulo = f.Titulo,
            Faturamento = f.Faturamento,
            Orcamento = f.Orcamento,
            Distribuidor = f.Distribuidor,
            Genero = f.Genero,
            Diretor = f.Diretor,
            Lucro = f.Faturamento - f.Orcamento,
            LucroPorcentagem = (f.Faturamento - f.Orcamento) / f.Orcamento
        };

    return consulta;
}

public static void Tarefa1(IEnumerable<Filme> filmes){
	var result = from f in filmes where f.Genero == "Adventure" select f;
	GeraRelatorio("Tarefa 1: obter a lista de filmes de Aventura", result);
}

public static void Tarefa2(IEnumerable<Filme> filmes){
	var result = from f in 
	filmes
	.AsParallel() //habilita o paralelismo, não necessarimente vai haver um paralelismo
	where f.Genero == "Adventure" select f;
	GeraRelatorio("Tarefa 2: obter a lista de filmes de Aventura, executando em PARALELO", result);
}

public static void Tarefa3(IEnumerable<Filme> filmes){
	//A consulta poderá ser executada com paralelismo, desde que isso resulte em algum ganho de desempenho.
	
	var result = from f in 
	filmes.AsParallel()
	.WithExecutionMode(ParallelExecutionMode.Default)//define o tipo de paralelismo
	where f.Genero == "Adventure" select f;
	GeraRelatorio("Tarefa 3: obter a lista de filmes de Aventura, executando em PARALELO com modo de execução default", result);
}

public static void Tarefa4(IEnumerable<Filme> filmes){
	var result = from f in filmes.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism)
	where f.Genero == "Adventure" select f;
	GeraRelatorio("Tarefa 4: obter a lista de filmes de Aventura, executando em PARALELO forçando paralelismo", result);
}


public static void Tarefa5(IEnumerable<Filme> filmes){
	var result = from f in 
		filmes
		.AsParallel()
		.WithExecutionMode(ParallelExecutionMode.ForceParallelism) //forma o paralelismo
		.WithDegreeOfParallelism(4) //define quantas tarefas em paralelo podem ser executadas ao mesmo tempo
	where f.Genero == "Adventure" select f;
	GeraRelatorio("Tarefa 5: obter a lista de filmes de Aventura, executando em PARALELO forçando paralelismo e com grau de paralelismo = 4", result);
}

public static void Tarefa6(IEnumerable<Filme> filmes){
	var result = from f in 
		filmes
		.AsParallel()
		.AsOrdered()
	where f.Genero == "Adventure" select f;
	GeraRelatorio("Tarefa 6: obter a lista de filmes de Aventura, executando em PARALELO e preservando a ordem", result);
}

public static void Tarefa7(IEnumerable<Filme> filmes){
	var result = (from f in 
		filmes
		.AsParallel()
	where f.Genero == "Adventure"
	orderby f.Faturamento descending
	select f).Take(4);
	GeraRelatorio("Tarefa 7: obter os 4 filmes de Aventura de maior faturamento, executando em PARALELO", result);
}

public static void Tarefa8(IEnumerable<Filme> filmes){
	var result = from f in 
		filmes
		.AsParallel()
	where f.Genero == "Adventure"
	select f;
	
	result.ForAll(filme => 
	{
		Console.WriteLine(filme.Titulo);
	});
	//GeraRelatorio("Tarefa 8: Imprimir somente os títulos dos filmes, de aventura, consultando em PARALELO e usando uma ação em PARALELO", result);
}


void Main()
{

    var filmes = GetFilmes();
		//Tarefa 1: obter a lista de filmes de Aventura 
        //Cronometro(Tarefa1, filmes);
		
        //Tarefa 2: obter a lista de filmes de Aventura, executando em PARALELO
		//Cronometro(Tarefa2, filmes);

        //Tarefa 3: obter a lista de filmes de Aventura, executando em PARALELO com modo de execução default
		//Cronometro(Tarefa3, filmes);
        
		//Tarefa 4: obter a lista de filmes de Aventura, executando em PARALELO forçando paralelismo
		//Cronometro(Tarefa4, filmes);
		
		//Tarefa 5: obter a lista de filmes de Aventura, executando em PARALELO forçando paralelismo e com grau de paralelismo = 4
		//Cronometro(Tarefa5, filmes);

        //Tarefa 6: obter a lista de filmes de Aventura, executando em PARALELO e preservando a ordem
		//Cronometro(Tarefa6, filmes);

        //Tarefa 7: obter os 4 filmes de Aventura de maior faturamento, executando em PARALELO
		//Cronometro(Tarefa7, filmes);
		
        //Tarefa 8: Imprimir somente os títulos dos filmes, de aventura, consultando em PARALELO e usando uma ação em PARALELO
		Cronometro(Tarefa8, filmes);
        //Console.WriteLine(consulta1);
       // Console.ReadLine();
    
}
// Define other methods and classes here
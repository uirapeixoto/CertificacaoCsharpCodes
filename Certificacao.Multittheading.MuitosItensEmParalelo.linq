<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.Parallel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Timer.dll</Reference>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static void Processar(object item)
        {
            Console.WriteLine($"Começando a trabalhar em: {item}");
            Thread.Sleep(100);
            Console.WriteLine($"Terminando a trabalhar em: {item}");
            Console.WriteLine();
		}
void Main()
{
		/*
			Muitas tarefas em paralelo, com pramâmetro
			Tarefa 1: processar 100 itens em série
			Tarefa 2: processar 100 itens em paralelo - percorrendo uma faixa
			Tarefa 3: processar 100 itens em paralelo - percorrendo uma coleção
		*/
		Cronometro(Tarefa02);
		Console.WriteLine($"Término do processamento. Tecle [ENTER] para terminar.");
		//Console.ReadLine();
}

static void ReturnNull()
{
}

static void Tarefa01()
{
	Console.WriteLine($"Processar 100 itens em série.");
		List<int> itens = Enumerable.Range(0,100).ToList();
		itens.ForEach(p => {
			Processar(p);
		});
}

private delegate void MeuDelegate();
static void Cronometro(MeuDelegate meuDelegate)
{

	Stopwatch cronometro = new Stopwatch();
	cronometro.Start();
		meuDelegate();
	cronometro.Stop();
	Console.WriteLine($"Tempo decorrido para a execução foi de {cronometro.ElapsedMilliseconds  / 1000.0} segundos.");
}

static void Tarefa02()
{
	Console.WriteLine($"Processar 100 itens em faixa.");
	Parallel.For(1,100,(i) => Processar(i));
}

static void Tarefa03()
{
	Console.WriteLine("Tarefa 3: processar 100 itens em paralelo - percorrendo uma coleção");
    var itens = Enumerable.Range(0, 100);
    Parallel.ForEach(itens, (item) => Processar(item));

}

// Define other methods and classes here
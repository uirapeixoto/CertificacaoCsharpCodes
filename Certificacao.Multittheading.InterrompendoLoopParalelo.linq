<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static void Processar(object item)
{
    Console.WriteLine($"Começando a trabalhar em: {item}");
    Thread.Sleep(100);
    Console.WriteLine($"Terminando a trabalhar em: {item}");
    Console.WriteLine();
}
private delegate void MeuDelegate();
static void Cronometro(MeuDelegate meuDelegate)
{

	Stopwatch cronometro = new Stopwatch();
	cronometro.Start();
		meuDelegate();
	cronometro.Stop();
	Console.WriteLine($"Tempo decorrido para a execução foi de {(cronometro.ElapsedMilliseconds  / 1000.0)} segundos.");
}


void Main()
{
		/*
			Muitas tarefas em paralelo, com pramâmetro
			Tarefa 1: processar uma faixa de 100 itens em paralelo
			Tarefa 2: Completou sem interrupção?
			Tarefa 3: Interromper quando começar a processar o valor 75
			Tarefa 4: Quantos itens foram processados (parcialmente)?
		*/
		Cronometro(Tarefa01);
		Console.WriteLine($"Término do processamento. Tecle [ENTER] para terminar.");
		//Console.ReadLine();
}

static void Tarefa01()
{
	Console.WriteLine($"Processar 100 itens em faixa.");
	var result = Parallel.For(0, 100,(int i, ParallelLoopState state) => {
		if(i == 75)
		{
			state.Break();
		}
			Processar(i);
		});
	Console.WriteLine($"Completou sem interrupção? {result.IsCompleted}");
	Console.WriteLine($"Tarefa 4: Quantos itens foram processados (parcialmente)? {result.LowestBreakIteration}");
}
a

// Define other methods and classes here
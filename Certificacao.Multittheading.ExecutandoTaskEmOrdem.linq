<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static void ExecutaTrabalho(int i)
{
    Console.WriteLine($"Começando a trabalhar em: {i}");
    Thread.Sleep(100);
    Console.WriteLine($"Terminando a trabalhar em: {i}");
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
		//Console.WriteLine($"Término do processamento. Tecle [ENTER] para terminar.");
		//Console.ReadLine();
}

static void Tarefa01()
{
	Task tarefa1 = new Task(() => ExecutaTrabalho(1));
	var result =  Task.FromResult(tarefa1);
	Console.WriteLine(result);
	
	Task tarefa2 = new Task(() => ExecutaTrabalho(2));
	tarefa2.Wait();
	tarefa2.Start();
	
	Task tarefa3 = new Task(() => ExecutaTrabalho(3));
	tarefa3.Wait();
	tarefa3.Start();
}



// Define other methods and classes here
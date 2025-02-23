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
	}

static void ExecutaTrabalho()
	{
	    Console.WriteLine($"Começando a trabalhar");
	    Thread.Sleep(100);
	    Console.WriteLine($"Terminando  trabalho");
	}

//Delegate com parametro
private delegate void MeuDelegate1(int i);
static void Cronometro(Action<int> meuDelegate, int i)
{
	Stopwatch cronometro = new Stopwatch();
	cronometro.Start();
	meuDelegate(i);
	cronometro.Stop();
	Console.WriteLine($"Tempo decorrido para a execução foi de {(cronometro.ElapsedMilliseconds  / 1000.0)} segundos.");
	Console.WriteLine();
}

//Delegate sem parametro
private delegate void MeuDelegate2();
static void Cronometro(MeuDelegate2 meuDelegate)
{

	Stopwatch cronometro = new Stopwatch();
	cronometro.Start();
	meuDelegate();
	cronometro.Stop();
	Console.WriteLine($"Tempo decorrido para a execução foi de {(cronometro.ElapsedMilliseconds  / 1000.0)} segundos.");
}

void Main()
{
   Cronometro(ExecutaTrabalho);
   Console.WriteLine();
   Cronometro(ExecutaTrabalho, 1);
}
// Define other methods and classes here
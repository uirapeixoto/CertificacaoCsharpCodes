<Query Kind="Program">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Collections.Concurrent.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

class MyClass
{
	public int Id {get; set;}
	public string Nome {get; set;}
	public DateTime Data {get; set;}
}

private static BlockingCollection<int> data = new BlockingCollection<int>();
private static void Producer(){
	for(int ctr = 0; ctr < 10; ctr++)
	{
		data.Add(ctr);
		Thread.Sleep(1000);
	}

}

private static void Consumer(){

	foreach(var item in data.GetConsumingEnumerable()){
		Console.WriteLine(item);
	}
}

void Main()
{
	Producer();
	Consumer();
}
// Define other methods and classes here
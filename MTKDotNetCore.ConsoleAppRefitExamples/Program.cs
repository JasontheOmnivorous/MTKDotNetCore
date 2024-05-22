using MTKDotNetCore.ConsoleAppRefitExamples;

try
{
    RefitExample refitExample = new RefitExample();
    await refitExample.RunAsync();

    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
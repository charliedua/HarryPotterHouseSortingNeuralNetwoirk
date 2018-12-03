namespace HarryPotterHouseSortingNeuralNetwoirk
{
    internal interface ICalculateError
    {
        float CalcError(float target);

        float CurrentError { get; }
    }
}
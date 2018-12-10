namespace NeuralNetworkLibrary
{
    internal interface ICalculateError
    {
        void CalcError();

        float CurrentTarget { get; set; }

        float CurrentError { get; }
    }
}
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CharlesCalc.ViewModels
{
    /// <summary>
    /// view model for Calculator; calculate equation, display shown to user, etc.
    /// </summary>
    public class CalculatorViewModel : BaseViewModel
    {
        bool operandSelected;
        /// <summary>
        /// if user used an operand, then next number tap will replace display
        /// </summary>
        bool clearOnNextTap;
        string currentOperand;

        /// <summary>
        /// ex. 2 + 3, 2 is left side of equation, 3 is right side of equation
        /// </summary>
        string leftSideofEquation, rightSideOfEquation;

        public CalculatorViewModel()
        {
            Title = $"Calculator - version {Xamarin.Essentials.VersionTracking.CurrentVersion}";
            EqualsCommand = new Command(CalculateEquation);
            ClearCommand = new Command(Clear);
            NumberTapCommand = new Command(TapNumber);
            PlusMinusCommand = new Command(PlusMinus);
            OperandCommand = new Command(Operand);
            PieCommand = new Command(Pie);
        }
        public ICommand EqualsCommand { get; set; }
        public ICommand PieCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand PlusMinusCommand { get; set; }
        public ICommand NumberTapCommand { get; set; }
        public ICommand OperandCommand { get; set; }

        string equation = "0";
        public string Equation
        {
            get { return equation; }
            set { SetProperty(ref equation, value); }
        }

        void TapNumber(object x)
        {
            if (clearOnNextTap)
                Equation = "";

            //display to user
            if(Equation == "0" && x.ToString() == "0")
            {
                Equation = "0";
            }
            else if(equation == "0" && x.ToString() != ".")
            {
                Equation = x.ToString();
            }
            else if(Equation == "0" && x.ToString() == ".")
            {
                Equation = $"0.";
                
            }
            else if (x.ToString() == ".")
            {
                if(!equation.Contains("."))
                    Equation = $"{equation}{x}"; //only 1 decimal per equation
            }
            else
            {
                Equation = $"{equation}{x}";
            }

            //equation setup
            if(!operandSelected)
            {
                leftSideofEquation = equation;
            }
            else
            {
                rightSideOfEquation = equation;
            }
            clearOnNextTap = false;
        }

        void Clear()
        {
            Equation = "0";
            operandSelected = false;
            rightSideOfEquation = leftSideofEquation ="";
        }

        void PlusMinus()
        {
            Equation = equation.StartsWith("-") ? equation.Replace("-","") : $"-{equation}";
            if (operandSelected)
                rightSideOfEquation = rightSideOfEquation.StartsWith("-") ? rightSideOfEquation.Replace("-", "") : $"-{rightSideOfEquation}";
            else
                leftSideofEquation = leftSideofEquation.StartsWith("-") ? leftSideofEquation.Replace("-", "") : $"-{leftSideofEquation}";
        }

        void Operand(object x)
        {
            if(!operandSelected)
            {
                operandSelected = true;
                currentOperand = x.ToString();
                clearOnNextTap = true;
            }
            else
            {
                CalculateEquation();
            }
        }

        void CalculateEquation()
        {
            try
            {
                if (operandSelected)
                {
                    switch (currentOperand)
                    {
                        case "+":
                            Equation = (double.Parse(leftSideofEquation) + double.Parse(rightSideOfEquation)).ToString();
                            break;
                        case "-":
                            Equation = (double.Parse(leftSideofEquation) - double.Parse(rightSideOfEquation)).ToString();
                            break;
                        case "×":
                            Equation = (double.Parse(leftSideofEquation) * double.Parse(rightSideOfEquation)).ToString();
                            break;
                        case "÷":
                            Equation = (double.Parse(leftSideofEquation) / double.Parse(rightSideOfEquation)).ToString();
                            break;
                    }
                    clearOnNextTap = true;
                    operandSelected = false;
                    rightSideOfEquation = "";
                    leftSideofEquation = equation;
                }
            }
            catch
            {
                //swallow exception for now, with more time would handle this accordingly
            }
            finally
            {
                clearOnNextTap = true;
                operandSelected = false;
                rightSideOfEquation = "";
                leftSideofEquation = equation;
            }
        }

        void Pie()
        {
            TapNumber(Math.PI);
        }
    }
}
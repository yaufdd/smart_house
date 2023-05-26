using System;
using System.Collections.Generic;

// Абстрактный класс команды
abstract class Command
{
    public abstract void Execute();
}

// Конкретная команда - Включить свет
class LightOnCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("Свет включен");
    }
}

// Конкретная команда - Выключить свет
class LightOffCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("Свет выключен");
    }
}

// Конкретная команда - Установить яркость
class SetBrightnessCommand : Command
{
    private readonly int brightness;

    public SetBrightnessCommand(int brightness)
    {
        this.brightness = brightness;
    }

    public override void Execute()
    {
        Console.WriteLine("Яркость установлена на: " + brightness);
    }
}

// Интерпретатор команд
class CommandInterpreter
{
    private readonly CommandBuilder commandBuilder;

    public CommandInterpreter(CommandBuilder commandBuilder)
    {
        this.commandBuilder = commandBuilder;
    }

    public void Interpret(string commandString)
    {
        // Разбиваем строку команды на отдельные части
        string[] commandParts = commandString.Split(' ');

        // Первая часть команды - тип команды
        string commandType = commandParts[0];

        // Создаем соответствующий объект команды с помощью строителя
        Command command;
        switch (commandType)
        {
            case "включить":
                command = commandBuilder.BuildLightOnCommand();
                break;
            case "выключить":
                command = commandBuilder.BuildLightOffCommand();
                break;
            case "установить":
                int brightness = int.Parse(commandParts[2]);
                command = commandBuilder.BuildSetBrightnessCommand(brightness);
                break;
            default:
                throw new ArgumentException("Недопустимая команда");
        }

        // Выполняем команду
        command.Execute();
    }
}

// Строитель команд
class CommandBuilder
{
    public LightOnCommand BuildLightOnCommand()
    {
        return new LightOnCommand();
    }

    public LightOffCommand BuildLightOffCommand()
    {
        return new LightOffCommand();
    }

    public SetBrightnessCommand BuildSetBrightnessCommand(int brightness)
    {
        return new SetBrightnessCommand(brightness);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем экземпляры строителя команд и интерпретатора
        CommandBuilder commandBuilder = new CommandBuilder();
        CommandInterpreter commandInterpreter = new CommandInterpreter(commandBuilder);

        // Примеры команд
        string command1 = "включить свет";
        string command2 = "установить яркость 50";
        string command3 = "выключить свет";

        // Интерпретируем и выполняем команды
        commandInterpreter.Interpret(command1);
        commandInterpreter.Interpret(command2);
        commandInterpreter.Interpret(command3);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

public class Consumidor {
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public int CPF { get; set; }
    public int Tipo { get; set; }

    public Consumidor(int id, string nome, int idade, int cpf, int tipo) {
        Id = id;
        Nome = nome;
        Idade = idade;
        CPF = cpf;
        Tipo = tipo;
    }

    public string GetNome() {
        return Nome;
    }

    public void SetNome(string nome) {
        Nome = nome;
    }

    public int GetTipo() {
        return Tipo;
    }

    public void SetTipo(int tipo) {
        Tipo = tipo;
    }
}
﻿using Domain.Base;
using Domain.Request;
using System.Text.RegularExpressions;

namespace Domain.Entities;

public class Client : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public Address Address { get; set; }

    public Client(Guid id, string nome, string cpf, string email)
    {
        Id = id;
        Name = nome;
        CPF = cpf;
        Email = email;

        ValidateEntity();
    }

    public Client() { }

    public static Client FromRequest(ClientRequest clientRequest)
    {
        var cpf = Regex.Replace(clientRequest.CPF, @"[.-]", "");

        return new Client
        {
            Name = clientRequest.Name,
            Email = clientRequest.Email,
            CPF = cpf,
            Address = new Address(
                clientRequest.AddressRequest?.Street, 
                clientRequest.AddressRequest?.City, 
                clientRequest.AddressRequest?.State, 
                clientRequest.AddressRequest?.ZipCode)
        };
    }

    public void ValidateEntity()
    {
        AssertionConcern.AssertArgumentNotEmpty(Name, "O nome não pode ser vazio");
        AssertionConcern.AssertArgumentNotEmpty(Email, "O Email não pode ser vazio");
        AssertionConcern.AssertArgumentNotEmpty(CPF, "O CPF não pode ser vazio");
    }
}
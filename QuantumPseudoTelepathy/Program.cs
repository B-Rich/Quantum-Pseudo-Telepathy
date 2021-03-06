﻿using System;
using System.Diagnostics;
using System.Linq;

public static class Program {
    public static void Main() {
        PseudoTelepathy.CheckAllGameRuns();

        //FindCircuitsForMatrices();

        RunAndPrintSampleGamesForever(new Random());
    }

    private static void RunAndPrintSampleGamesForever(Random rng) {
        Console.WriteLine("Game:");
        Console.WriteLine("- Referee picks random row and col.");
        Console.WriteLine("- Alice is told row and covers 0 or 2 cells in row with 'A' tokens.");
        Console.WriteLine("- Bob is told col and covers 0 or 2 cells in col with 'B' tokens.");
        Console.WriteLine("- (They can't communicate, but they can have pre-shared entangled qubits.)");
        Console.WriteLine("- Alice and Bob win if their common cell has exactly one token on it");
        Console.WriteLine("- (Without entangled qubits they can't win CONSISTENTLY. Can they do it with???)");
        Console.WriteLine();
        while (true) {
            Console.WriteLine("Press enter to run a game...");
            Console.ReadLine();
            PseudoTelepathy.RunAndPrintSampleGame(rng);
        }
    }

    private static void FindCircuitsForMatrices() {
        FindPrintCircuitMatchingMatrix(PseudoTelepathyCircuits.AliceTopRow);
        FindPrintCircuitMatchingMatrix(PseudoTelepathyCircuits.AliceCenterRow);
        FindPrintCircuitMatchingMatrix(PseudoTelepathyCircuits.AliceBottomRow);
        FindPrintCircuitMatchingMatrix(PseudoTelepathyCircuits.BobLeftColumn);
        FindPrintCircuitMatchingMatrix(PseudoTelepathyCircuits.BobCenterColumn);
        FindPrintCircuitMatchingMatrix(PseudoTelepathyCircuits.BobRightColumn);

        while (true) {
            Console.WriteLine("Done. Hit Enter Twice to Continue.");
            Console.ReadLine();
            Console.ReadLine();
        }
    }

    private static void FindPrintCircuitMatchingMatrix(ComplexMatrix target) {
        var targetDesc = 
            "-----" + Environment.NewLine 
            + "Searching for circuits that match:" + Environment.NewLine 
            + "-----" + Environment.NewLine 
            + target.ToString() + Environment.NewLine 
            + " ----- (may take awhile)...";
        Console.WriteLine(targetDesc);
        Debug.WriteLine(targetDesc);

        var r = from circuit in Circuits.DistinctCircuitsUpToSize4_Cache()
                where circuit.Value.IsMultiRowPhased(target) // for pseudotelepathy, thw row phases of each matrix don't matter
                select circuit.Key.Select((e,i) => "Gates." + e + (i == 0 ? "" : ")")).StringJoin(".Then(");
        foreach (var c in r.Take(16)) {
            Console.WriteLine(c);
            Debug.WriteLine(c);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the farming system, including animal management, feeding, and interactions.
/// </summary>
public class FarmingSystem : MonoBehaviour
{
    /// <summary>
    /// List of all animals in the farming system.
    /// </summary>
    private List<Animal> animals = new List<Animal>();

    /// <summary>
    /// Initializes the farming system with some default animals.
    /// </summary>
    private void Start()
    {
        // Example initialization
        animals.Add(new Animal
        {
            Id = 1,
            Name = "Pet Cat",
            Type = AnimalType.Pet,
            GrowthTime = 0,
            MaturityCycle = 0,
            Outcome = null,
            AssetPath = "assets/animals/cat"
        });

        animals.Add(new Animal
        {
            Id = 2,
            Name = "Dairy Cow",
            Type = AnimalType.Cattle,
            GrowthTime = 604800, // 7 days
            MaturityCycle = 86400, // 1 day
            Outcome = OutcomeType.Milk,
            AssetPath = "assets/animals/cow"
        });
    }

    /// <summary>
    /// Feeds the specified animal with the given feed type.
    /// </summary>
    /// <param name="animalId">The ID of the animal to feed.</param>
    /// <param name="feedType">The type of feed to use.</param>
    public void FeedAnimal(int animalId, FeedType feedType)
    {
        Animal animal = animals.Find(a => a.Id == animalId);
        if (animal != null)
        {
            // Implement feeding logic here
            Debug.Log($"Feeding {animal.Name} with {feedType} feed.");
        }
        else
        {
            Debug.LogError("Animal not found!");
        }
    }

    /// <summary>
    /// Interacts with the specified animal using the given interaction type.
    /// </summary>
    /// <param name="animalId">The ID of the animal to interact with.</param>
    /// <param name="interactionType">The type of interaction to perform.</param>
    public void InteractWithAnimal(int animalId, InteractionType interactionType)
    {
        Animal animal = animals.Find(a => a.Id == animalId);
        if (animal != null)
        {
            // Implement interaction logic here
            Debug.Log($"Interacting with {animal.Name} using {interactionType}.");
        }
        else
        {
            Debug.LogError("Animal not found!");
        }
    }

    /// <summary>
    /// Harvests the outcome from the specified animal.
    /// </summary>
    /// <param name="animalId">The ID of the animal to harvest from.</param>
    public void HarvestAnimal(int animalId)
    {
        Animal animal = animals.Find(a => a.Id == animalId);
        if (animal != null && animal.Outcome.HasValue)
        {
            // Implement harvesting logic here
            Debug.Log($"Harvesting {animal.Outcome} from {animal.Name}.");
        }
        else
        {
            Debug.LogError("Animal not found or no outcome available!");
        }
    }
}

/// <summary>
/// Represents an animal in the farming system.
/// </summary>
public class Animal
{
    public int Id { get; set; }                  // Unique ID for the animal
    public string Name { get; set; }             // Name of the animal
    public AnimalType Type { get; set; }         // Type of the animal
    public int GrowthTime { get; set; }          // Time required for the animal to grow
    public int MaturityCycle { get; set; }       // Cycle time for maturity
    public OutcomeType? Outcome { get; set; }    // Type of outcome produced by the animal
    public string AssetPath { get; set; }        // Path to the animal's asset
}

/// <summary>
/// Enum for different types of animals.
/// </summary>
public enum AnimalType
{
    Pet,        // Pets like cats and dogs
    Cattle,     // Livestock like chickens, cows, and sheep
    Special     // Special animals like holiday-themed ones
}

/// <summary>
/// Enum for different types of interactions with animals.
/// </summary>
public enum InteractionType
{
    Pet,       // Petting the animal
    Feed,      // Feeding the animal
    Clean,     // Cleaning the animal's pen
    Harvest    // Harvesting products from the animal
}

/// <summary>
/// Enum for different types of feed.
/// </summary>
public enum FeedType
{
    Normal,    // Normal feed
    Rare,      // Rare feed
    Winter     // Winter-specific feed
}

/// <summary>
/// Enum for different types of outcomes from animals.
/// </summary>
public enum OutcomeType
{
    Egg,       // Eggs from chickens
    Milk,      // Milk from cows
    Wool,      // Wool from sheep
    Meat,      // Meat from pigs
    RareWool   // Rare wool from special sheep
} 
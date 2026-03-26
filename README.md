# Erase

**Erase** is a first-person VR mystery / discovery game developed as a Human-Computer Interaction project.  
The player takes the role of a survivor who returns to the past to investigate a school and prevent a catastrophe caused by a toxic gas leak.

The project was designed under **User-Centered Design (UCD)** principles, with a strong focus on:

- immersive first-person exploration,
- intuitive object interaction,
- cognitive support through an inventory / clue journal,
- and VR comfort, especially minimizing motion sickness.

## Concept

The story begins with newspaper headlines describing a tragic accident at a school. A toxic gas leak, initially reported as a technical failure, caused a catastrophe. The player then discovers that they were supposed to be there that day, but avoided the incident by chance.

Now, through a portal to the past, the player returns to the school on the morning of the disaster with one mission:

**find the source of the gas leak and stop it before it happens.**

To do that, the player must explore the school, collect evidence, inspect clues, manage information, and use key tools such as a gas mask and a gas sensor to access dangerous zones and solve the final mystery.

## Design Goals

Erase was built around four core goals:

1. **Deliver an intellectually satisfying mystery experience**  
   The game is meant to reward logical deduction instead of trial and error.

2. **Ensure comfort and usability in VR**  
   The experience prioritizes stable performance, intuitive controls, and locomotion choices that reduce discomfort.

3. **Promote player autonomy**  
   Players should feel in control of their investigation through natural interaction with the environment and objects.

4. **Use exploration as narrative delivery**  
   The story is primarily communicated through environmental details, notes, newspapers, and diegetic feedback instead of intrusive exposition.

## Core Gameplay Loop

The gameplay is structured around the following loop:

**Navigate → Interact → Collect → Deduce**

This loop reflects the project’s main HCI pillars:

- direct interaction with the world,
- external cognitive support through the clue journal,
- contextual onboarding,
- and narrative immersion through environmental storytelling and feedback.

## Main Features

### 1. First-person VR exploration

Players navigate a 3D school environment in first person.  
Locomotion was designed with VR comfort in mind, prioritizing lower discomfort and better immersion.

### 2. Environmental interaction

The player can manipulate the environment directly, including interactions such as opening doors and engaging with important scene elements.

### 3. Object handling

Key objects can be picked up, inspected, and dropped.  
This supports natural interaction and reinforces recognition over recall.

### 4. Inventory / clue journal

A central design feature of the project is a system for storing and reviewing collected clues such as:

- newspaper clippings,
- notes,
- manuals,
- and other pieces of evidence.

This reduces cognitive load and helps the player focus on reasoning rather than memorization.

### 5. Gas danger system

Invisible environmental danger is communicated through multimodal feedback.  
The game includes mechanics tied to hazardous zones and protective tools such as the gas mask.

### 6. Contextual onboarding

Instead of relying on a long traditional tutorial, the game introduces mechanics when the player first needs them, preserving immersion while still helping less experienced users.

### 7. Pause menu

The experience includes a pause system so the player can temporarily stop without losing progress.

## Target Audience

Erase is aimed primarily at players between **15 and 35 years old** who have access to a **Meta Quest** headset and are interested in mystery, suspense, and puzzle-solving experiences.

The design especially considers two user profiles:

- **The enthusiast detective**: a player who enjoys deduction, logical puzzle solving, and rich clue management systems.
- **The casual VR player**: a player drawn by novelty and immersion, who needs intuitive controls and low-friction onboarding.

## HCI / UX Foundations

This project was developed as part of an HCI course and explicitly draws on **User-Centered Design** principles.  
Several design decisions were made to align with the needs of the project persona:

- avoid intrusive tutorials,
- reduce motion sickness,
- keep text readable in VR,
- make interactions feel direct and physical,
- and provide support systems that lower cognitive overload.

A major design concern was balancing **immersion**, **narrative tension**, and **usability** in a standalone VR context.

## Technical Constraints and Requirements

The project was defined with the following major technical goals:

- use the **Meta SDK**,
- run natively on **Meta Quest 2**,
- maintain at least **60 FPS** to reduce motion sickness,
- provide intuitive navigation and controls,
- avoid crashes during standard play sessions,
- and keep UI text readable in VR.

Additional quality goals include optimized assets for standalone VR hardware and acceptable loading times.

## Narrative Structure

The player’s arc follows a classic **redemption** structure:

- **Beginning**: the player is a passive survivor, burdened by guilt.
- **Middle**: the player becomes an active investigator by traveling to the past.
- **End**: the player prevents the disaster and becomes a redeemed hero.

The game uses:

- first-person perspective,
- subtle environmental storytelling,
- diegetic feedback,
- and tension-building through contextual visual and audio cues.

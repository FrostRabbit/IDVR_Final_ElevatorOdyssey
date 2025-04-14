# Elevator Odyssey

[YouTube Demo Video](https://youtu.be/gn-GAHq7tp4)

## Introduction

In this busy society, many people hardly have a moment to rest or relax. Even while taking the elevator, many are already thinking about their upcoming meetings, how to prepare work reports, or the looming deadlines. To help everyone find brief moments of relaxation, we propose this plan: allowing users to experience a variety of scenic views during their elevator rides, offering their minds and bodies a small but meaningful pause to catch their breath.

### Here are some experiences you can enjoy while riding the elevator:

- Relax as you select your floor. Then immerse yourself in the unique charm of foreign cultures.
- Forget your busy schedule and give your mind and body a refreshing break.

## Features

### Immersive Scenic Experiences

MR technology transforms the elevator interior into virtual environments like beaches, forests, or starry skies, offering a calming escape from stress.

### Making Wait Time Enjoyable

While waiting for the elevator to reach your floor, MR provides interactive content, such as mini-games or quick learning opportunities, turning dull moments into fun ones.

### Boosting Creativity 

Explore art pieces or creative scenes through MR to spark fresh ideas—perfect for moments when you need inspiration.

### Reducing Crowding Anxiety

MR creates the illusion of open spaces through visual effects, alleviating feelings of confinement and enhancing overall comfort, even in crowded elevators.

### A Tool for Relaxation and Meditation

MR integrates guided breathing exercises and calming visuals, helping users relax quickly and ease daily stress.

## Usage Method

The focus of our project is on creating an immersive experience. Users only need to wear an MR headset, step into the elevator, and select their desired floor. From there, they can begin exploring the exotic cultures or breathtaking landscapes we’ve designed specifically for them.

## Themed Floor Experience

1. Basement  
2. Desert  
3. Manor  
4. Oasis  
5. Grassland  
6. Cinema  
7. Space  

## Detect Elevator Doors Open / Close

Using Meta's built-in depth detection feature, determine the distance of objects in front of the headset.  

- If the distance exceeds a certain value, it indicates that the elevator doors are open.  
- If the headset detects the elevator doors, it indicates that the doors are closed.

## Virtual World Fade In / Fade Out

1. **Passthrough as the Background**  
   The real-world passthrough feed is rendered as the background using the Passthrough Layer (e.g., `OVRPassthroughLayer` for Meta Quest devices).

2. **Virtual Scene Objects**  
   Virtual objects (like 3D models or environments) are rendered on top of the passthrough background.

3. **Transparency Control**  
   Adjust the alpha (transparency) of object materials (e.g., `Material.color.a`) to smoothly fade virtual objects in or out.

4. **Fade In / Fade Out**  
   - When the elevator doors open, the virtual scene fades out to show the real world.  
   - After selecting a floor, the themed virtual scene fades in until it fully replaces the real-world view.

## Virtual Button Setup

Use `PokeInteractable` to trigger the elevator movement.

```csharp
private PokeInteractable _elevatorbutton;

_elevatorbutton.WhenStateChanged += WhenButtonStateChanged;

private void WhenButtonStateChanged(InteractableStateChangeArgs obj)
{
    if (obj.NewState == InteractableState.Select)
    {
        // Change floor
        Elevator_switch_passthrough.instance.switch_to_virtualworld(button_id);
    }
}
```

## Using Skybox

MR can't use skybox directly, as it covers passthrough. Instead, modify `OVRPassthroughLayer`:

```csharp
OVRPassthroughLayer _passthroughLayer;

_passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed;
_passthroughLayer.overlayType = OVROverlay.OverlayType.Underlay;
Camera.main.clearFlags = CameraClearFlags.SolidColor;
```

# BAM(Bohdan's Armageddon) — Bolide & Asteroid Modeling
[![C#](https://img.shields.io/badge/C%23-.NET%20Framework%20%2F%20.NET-239120?style=flat-square&logo=csharp&logoColor=white)](https://dotnet.microsoft.com/)
[![OpenGL](https://img.shields.io/badge/OpenGL-3.3%20Core-5586A4?style=flat-square&logo=opengl&logoColor=white)](https://www.opengl.org/)
[![OpenTK](https://img.shields.io/badge/OpenTK-3D%20Graphics-007acc?style=flat-square)](https://opentk.net/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?style=flat-square&logo=windows&logoColor=white)](https://www.microsoft.com/windows)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)](./LICENSE)
[![Conference](https://img.shields.io/badge/Publication-MicroCAD%202025%20(p.%201304)-orange?style=flat-square&logo=academia)](https://ndch.kpi.kharkov.ua/wp-content/uploads/2025/06/Zbirnik-tez-2025.pdf#page=1304)

A desktop simulation for procedural asteroid generation, atmospheric entry physics, and 3D impact crater visualization.

---

## Under the Hood (How it works)

* **Real-Time Physics Solver:** Calculates atmospheric deceleration, drag, heating, and thermal ablation (mass loss) step-by-step as the bolide falls through the atmosphere.
* **Hardware-Accelerated Rendering:** Uses OpenTK and custom GLSL shaders to render 3D crater deformations directly on the GPU, completely avoiding CPU bottlenecks.
* **Procedural Mesh Generation:** Builds unique, irregular asteroid meshes using 3D Perlin noise and harmonic distortions. Fully exportable as standard `.obj` files.
* **Empirical Impact Math:** Maps final kinetic energy and velocity to TNT equivalence to generate scientifically grounded crater dimensions, rim heights, and ejecta blankets.

---

## Core Features

| Module | What it does |
| :--- | :--- |
| **Procedural Asteroids** | Generates non-spherical 3D asteroid meshes with parametric craters (previewed via WPF HelixToolkit). |
| **Entry Simulator** | Simulates the descent trajectory, dynamically tracking velocity, aerodynamic drag, and vaporization thresholds. |
| **Crater Engine** | Estimates transient cavity, rim height, and uplift structures based on impact kinetics. |
| **3D Visualizer** | Renders asymmetrical crater grids and directional impact vectors in a custom OpenGL viewport. |

---

## The Physics

The engine dynamically calculates key trajectory metrics using real-world equations:

* **Atmospheric Drag:**
  $$F_d = \frac{1}{2} C_d \rho(h) A v^2$$
* **Radiative Cooling (Stefan-Boltzmann):**
  $$P_{rad} = \epsilon \sigma A_{surf} T^4$$
* **Impact Energy Equivalent:**
  $$E_k = \frac{1}{2} m v_{impact}^2 \quad \longrightarrow \quad Y_{TNT} = \frac{E_k}{4.184 \times 10^9 \text{ J/ton}}$$

---

## Tech Stack

* **Platform:** .NET Framework / Windows Forms
* **Graphics:** OpenTK (OpenGL 3.3 Core Profile, GLSL)
* **UI & Viewports:** [Helix Toolkit WPF](https://github.com/helix-toolkit/helix-toolkit), Guna.UI2
* **Export:** Wavefront 3D Object (`.obj`)

---

## Getting Started

### Prerequisites
* Visual Studio 2022 with **.NET desktop development** workload.
* GPU supporting OpenGL 3.3 Core Profile or higher.

### Building
1. Clone the repository:
   ```bash
   git clone [https://github.com/bohdandash/BAM.git](https://github.com/bohdandash/BAM.git)

### Preview
### Preview & Architecture

1. **Main Dashboard:**  
   ![Main Screen](./images/Main%20Screen.png)  
   *Initial setup interface for physical boundary conditions, material properties, and simulation control.*

2. **Procedural Asteroid Synthesis:**  
   ![Asteroid Creation](./images/Asteroid%20Creation.png)  
   *Parametric 3D mesh synthesis and procedural surface deformation in real time.*

3. **Trajectory & Atmospheric Simulation:**  
   ![Simulation Screen](./images/Simulation%20Screen.png)  
   *Numerical computation of atmospheric drag, energy dissipation, and trajectory coordinates.*

4. **Visual Analysis & Impact Results:**  
   ![Visualization Screen](./images/Visualization%20Screen.png)  
   *Rendered 3D visual feedback, surface morphology inspection, and deformation output via OpenGL.*


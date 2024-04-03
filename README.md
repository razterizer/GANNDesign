# GANNDesign

This is a GUI that allows you to design 2D GANN creatures for [`GANNSim`](https://github.com/razterizer/GANNSim).

![image](https://github.com/razterizer/GANNDesign/assets/32767250/bddc199e-6976-4704-b30f-c56681046c26)
![image](https://github.com/razterizer/GANNDesign/assets/32767250/347fa69b-dba2-42b3-bfbd-78b806bb128b)
![image](https://github.com/razterizer/GANNDesign/assets/32767250/92d05a81-b4ff-4db9-8f6a-2597d248a36a)
![image](https://github.com/razterizer/GANNDesign/assets/32767250/829a3e65-837e-4536-bee7-e4f161661c35)
![image](https://github.com/razterizer/GANNDesign/assets/32767250/0b28d68e-25b3-4102-83d1-8325c864ddb6)

There are currently 6 fixed input parameters to the MLP network brain to choose from:
* `centroid x position`
* `centroid y position`
* `centroid x velocity`
* `centroid y velocity`
* `mean angle`
* `mean ang. vel`

Then these are the dynamic input parameters that depend on the number of joints and muscles (linar or angular) that you have in the body of the creature:
* `joint contact i`
* `linear muscle length j`
* `linear muscle velocity j`
* `angular muscle angle k`
* `angular muscle ang.vel. k`

for joint `i`, linear muscle `j` and angular muscle `k`.

To make a linear or angular spring a muscle, you need to click on the spring and check the `Is Muscle` checkbox:
![image](https://github.com/razterizer/GANNDesign/assets/32767250/88b4e9b0-30a9-4090-a8f9-79669d699255)
![image](https://github.com/razterizer/GANNDesign/assets/32767250/d10d2a81-d6a3-4f5d-b3e2-4230af514b85)


## How to Build and Run

This program requires you to build a dll from [`CSMathLib`](https://github.com/razterizer/CSMathLib) and to add that as a reference in the `csproj`-project of this repo. Then just build and run.

import os

Ligands = os.listdir("./ligands")

Targets = os.listdir("./target")

print("""Hello There, 

My name is drug designo, i am script written to perform molecular docking using AutoDock Vina tool

i found the following Ligands:""")

print(Ligands)

print("Also i found the following Receptors:")

for f in Targets:
    if ".pdbqt" in f:
        print(f)
try:
    with open("grid_box.txt", "r") as sdf:
        lines = sdf.readlines()

    temp = lines[2].split()

    x_size = temp[1]
    y_size = temp[2]
    z_size = temp[3]

    temp = lines[3].split()

    x_value = temp[1]
    y_value = temp[2]
    z_value = temp[3]

    print("To make your life easy i found the following parameters in your grid_box.txt: x,y,z size search: ")
    print(x_size, y_size, z_size)
    print("Also your x,y,z values are:")
    print(x_value,y_value,z_value)


except:

    x_value = input("Kindly input your X coordinate of the center: ")
    y_value = input("Kindly input your y coordinate of the center: ")
    z_value = input("Kindly input your z coordinate of the center: ")
    x_size = input("Kindly input your size in the X dimension: ")
    y_size = input("Kindly input your size in the y dimension: ")
    z_size = input("Kindly input your size in the z dimension: ")


for f in Targets:
    if "pdbqt" in f:
        for lig in Ligands:
            os.system("autodock_vina_1_1_2_linux_x86/bin/vina --receptor target/" + f + " --ligand ligands/" + lig + " --center_x " + str(x_value) + " --center_y " + str(y_value) + " --center_z " + str(z_value) + " --size_x " + str(x_size) + " --size_y " + str(y_size) + " --size_z " + str(z_size) + " --out out/vina_" + lig + ".pdbqt --log Log/vina_" + lig + ".txt")
    else:
        print("Can't use this file as target" + f)


ligand_1 = ""

out = os.listdir("./out")

for pdbqt in out:
    os.system("autodock_vina_1_1_2_linux_x86/bin/vina_split --input out/" + pdbqt)
    ligand_1 = ligand_1 + "out/" + pdbqt[:-6] + "_ligand_1.pdbqt "

os.system("babel -ipdbqt " + ligand_1 + " -osdf all.sdf")

with open("all.sdf", "r") as sdf:
    lines = sdf.readlines()
temp = 0
for line in range(len(lines)):
    if "VINA RESULT" in lines[line]:
        temp = line
        lines[temp + 4] = "\n" + ">  <Score> \n" + lines[line].split()[2] + "\n"
        lines[temp + 5] = "\n" + lines[temp + 5]
with open("news.sdf", "w") as ss:
    ss.writelines(lines)

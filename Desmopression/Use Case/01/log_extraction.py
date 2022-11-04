import os

log = os.listdir("./Log")

names = {}

for files in log:
    tmp = files[5:]
    tmp = tmp[:-10]
    with open("Log/"+files, "r") as tmpr:
        lines = tmpr.readlines()
        for line in lines:
            if line.startswith("   1"):
                names[tmp] = line.split()[1]

with open("Output.csv", "w") as tmpw:
    for i in names:
        tmp = i,",", names[i], "\n"

        tmpw.writelines(tmp)

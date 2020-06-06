import matplotlib.pyplot as plt
import pyperclip

def cmapToCode(cmapName="inferno"):
    cmap = plt.get_cmap(cmapName)

    out = f"""\n        // {cmapName}\n"""
    out += """        public readonly byte[,] RGB =\n        {\n"""
    for i in range(256):
        r, g, b, a = cmap(i/255.0)
        out += "            {%d, %d, %d},\n" % (r*256, g*256, b*256)
    out += "        };\n"

    print(out)
    pyperclip.copy(out)
    print(f"Colormap {cmapName} copied to clipboard.")


if __name__ == "__main__":
    cmapToCode("cividis")
    # maps = sorted(m for m in plt.cm.datad if not m.endswith("_r"))
    # for cmap in maps:
    #     cmapToCode(cmap)

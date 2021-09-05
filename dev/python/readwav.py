"""
sample rate: 44100
values: 166671
value 12345: 4435
"""
from scipy.io import wavfile
import pathlib
PATH_HERE = pathlib.Path(__file__).parent
PATH_DATA = PATH_HERE.joinpath("../../data")

if __name__ == "__main__":
    for wavFilePath in PATH_DATA.glob("*.wav"):
        wavFilePath = PATH_DATA.joinpath(wavFilePath)
        samplerate, data = wavfile.read(wavFilePath)
        print(f"{wavFilePath.name}, {samplerate}, {len(data)}")

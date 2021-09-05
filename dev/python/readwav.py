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
    wavFilePath = PATH_DATA.joinpath("cant-do-that-44100.wav")
    samplerate, data = wavfile.read(wavFilePath)
    print(f"sample rate: {samplerate}")
    print(f"values: {len(data)}")
    print(f"value 12345: {data[12345]}")

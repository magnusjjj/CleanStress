from threading import Thread
import time


def OfThread(num):
    while True:
        print("in thread abc3", num)
        time.sleep(0.5)

def start(num):
    thread = Thread(target=OfThread, args=(num,))
    thread.start()
    time.sleep(5)
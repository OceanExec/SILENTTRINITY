from multiprocessing import Process
from multiprocessing.connection import Client
from core.state import ipc_pass


class Listener:

    def __init__(self):
        self.name = ''
        self.author = ''
        self.description = ''
        self.running = False
        self.options = {}
        self.__conn = None
        self.__thread = None

    def run(self):
        return

    def __run(self):
        self.__conn = Client(('localhost', 60000), authkey=ipc_pass)
        self.run()

    def start(self):
        self.__thread = Process(target=self.__run, daemon=True)
        self.__thread.start()
        self.running = True

    def dispatch_event(self, event, msg):
        self.__conn.send((event, msg))
        data = ()

        try:
            data = self.__conn.recv()
        except EOFError:
            pass

        return data

    def stop(self):
        self.__thread.kill()
        self.running = False

    def __getitem__(self, key):
        return self.options[key]['Value']

    def __setitem__(self, key, value):
        self.options[key]['Value'] = value

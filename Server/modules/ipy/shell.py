class STModule:
    def __init__(self):
        self.name = 'ipy/shell'
        self.language = 'ipy'
        self.description = 'Runs a shell command'
        self.author = '@byt3bl33d3r'
        self.options = {
            'Command': {
                'Description'   :   'The ShellCommand to execute, including any arguments',
                'Required'      :   True,
                'Value'         :   ''
            },
            'Path': {
                'Description'   :   'The Path of the directory from which to execute the ShellCommand',
                'Required'      :   False,
                'Value'         :   "C:\\\\WINDOWS\\\\System32\\\\"
            },
            'Username': {
                'Description'   :   'Optional alternative username to execute ShellCommand as',
                'Required'      :   False,
                'Value'         :   ""
            },
            'Domain': {
                'Description'   :   'Optional alternative Domain of the username to execute ShellCommand as',
                'Required'      :   False,
                'Value'         :   ""
            },
            'Password': {
                'Description'   :   'Optional password to authenticate the username to execute the ShellCommand as',
                'Required'      :   False,
                'Value'         :   ""
            }
        }

    def payload(self):
        with open('modules/ipy/src/shell.py', 'r') as module_src:
            src = module_src.read()
            src = src.replace("COMMAND_TO_RUN", self.options['Command']['Value'])
            src = src.replace("PATH", self.options['Path']['Value'])
            src = src.replace("USERNAME", self.options['Username']['Value'])
            src = src.replace("DOMAIN", self.options['Domain']['Value'])
            src = src.replace("PASSWORD", self.options['Password']['Value'])
            return src

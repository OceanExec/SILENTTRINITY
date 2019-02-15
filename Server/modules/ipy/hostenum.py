#Based on https://github.com/threatexpress/red-team-scripts
class STModule:
    def __init__(self):
        self.name = 'ipy/hostenum'
        self.language = 'ipy'
        self.description = 'Enumerates host configuration.'
        self.author = '@daddycocoaman'
        self.options = {
            'EnumLevel': {
                'Description'   :   'Specifies the level of enumeration to perform (quick/full)',
                'Required'      :   True,
                'Value'         :   "quick"
            }
        }

    def payload(self):
        with open('modules/ipy/src/hostenum.py', 'r') as module_src:
            src = module_src.read()
            src = src.replace("ENUM_LEVEL=", f"ENUM_LEVEL='{self.options['EnumLevel']['Value']}'")
            return src
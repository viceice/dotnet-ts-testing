import ts from "typescript";

log(`typescript v${ts.version}`)

export function transform(input: string, config: object, fileName: string) {
  log("compiling");
  fileName = fileName || "dummy.ts";
  const transpileOptions: ts.TranspileOptions = {
    fileName,
    compilerOptions: {
      ...config,
      target: ts.ScriptTarget.ES2015,
      module: ts.ModuleKind.ES2015,
      isolatedModules: true,
      esModuleInterop: true,
      types: [],
      lib: ["ES2015"]
    },
    reportDiagnostics: false
  };

  try {
    const res = ts.transpileModule(input, transpileOptions);
    if (res.diagnostics && res.diagnostics.length) {
      log("diag:\n");
      for (var d of res.diagnostics) {
        log(`[${ts.DiagnosticCategory[d.category]}]${d.file.fileName}:${d.file.getLineAndCharacterOfPosition(d.start).line}: ${d.messageText} (${d.code})`);
      }
    }
    return res.outputText;
  } catch (ex) {
    // Parsing stack is extremely long and not very useful, so just rethrow the message.
    throw new Error(ex.message);
  }
}


export default transform;
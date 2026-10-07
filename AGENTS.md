## Agent Guidelines

- Work on only one task at a time.
- Analyze the existing files before making changes.
- Do not delete resources without human approval.
- Do not modify `ProjectSettings` unless necessary and explicitly approved by a human.
- Do not install packages without explicit approval.
- Do not run the `eval` or `eval_file` commands.
- Commit and push regularly so work is not lost (standing approval from the human, given for this project).
  - Commit after each completed task, and also after meaningful intermediate progress. Do not leave work uncommitted at the end of a task.
  - Push right after committing: `git push -u origin <branch>` to the branch assigned to the session. Never push to `main` or to another branch.
  - Use short, descriptive commit messages. Stage only files related to the task.
  - Never force-push, and never rewrite history that has already been pushed.
  - Do not commit generated or editor files (`Library/`, `Temp/`, `Logs/`, `UserSettings/`).
  - Do not open a pull request unless the human explicitly asks.
  - This standing approval covers only commits and pushes. Other restrictions in this file still need explicit approval (deleting resources, `ProjectSettings`, packages).
- After making changes, check compilation and the console for errors.
- Provide a list of all changed files.
- Stop after completing the task.
- Do not add advertisements, in-app purchases, tracking, or external links.

How to Report a Bug If somethings fails then congrats, you found a bug. Useful bug reports are ones that get bugs fixed. A useful bug report is...

Reproducible - If we cant conclusively prove that it exists, we will probably stamp it "WORKSFORME" or "INVALID", and move on to the next bug. Specific - The quicker the we can trace down the issue to a specific problem, the more likely it'll be fixed expediently. So the goals of a bug report are to:

Pinpoint the bug Explain it to the us Your job is to figure out exactly what the problem is.

Bug Reporting General Guidelines Avoid duplicates: Search before you file!

Always test the latest available build.

One bug per report.

State useful facts, not opinions or complaints.

Flag security/privacy vulnerabilities as non-public.

How to Write a Good Bug Report A good bug report should include the following information:

Summary The goal of summary is to make the report searchable and uniquely identifiable.

A bad example: Shit doesnt work

A good Example: Selecting Hammer & Blade and trying to use Blood spells in APL crashes in Chrome.

Overview/Description The overview or description of a bug report is to explain the bug to the developer, including:

Abstracted summary of behavior (e.g. interpretation of test failures)

Justifications of why this is a bug

Any relevant spec links

Interpretation of the spec

Information on other implementations

Steps to Reproduce The goal of reproducible steps is to teach developer to recreate the bug on his own system. It may be as simple as Load the attached testcase in Browser XYZ. A more complex case may involve multiple steps, such as:

Step 1: Selecting this & that in the importview.

Step 2: Scrolling to the bottom of the page

Step 3: Clicking start.

Step 4: Spamming back/forward.

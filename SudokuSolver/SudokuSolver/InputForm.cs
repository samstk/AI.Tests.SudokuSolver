using SSDK.AI.Agent;
using SSDK.AI.Agent.Info;
using SSDK.AI.Agent.Solvers;
using SSDK.AI.Agent.Solvers.Elimination;
using System.Runtime.InteropServices;

namespace SudokuSolver
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
            AgentThread = new Thread(Solve);
            AgentThread.Start();
            sudokuGrid1.Value = "      43 \n  3    81\n 51  4  6\n6  1    9\n   58  12\n2   4   8\n56    19 \n3  9  8  \n  76     ";
            NextPuzzle = sudokuGrid1.Value;
        }

        public static Thread AgentThread;
        public static bool IsAlive = true;
        public static string NextPuzzle;
        public static string Output = "";

        public class SudokuProblem : AgentProblemSpace
        {

            public string State;

            public SudokuProblem(string state)
            {
                State = state;
            }

            public override double DistanceTo(AgentProblemSpace space)
            {
                return 0; // Not used
            }

            public override double Desirability(Agent agent)
            {
                int total = 0;
                for (int i = 0; i < State.Length; i++)
                    if (State[i] == ' ')
                        total++;
                return 1 - total / 81f;
            }

            public override void Perceive(Agent agent)
            {
                // Not used
            }

            public char GetValue(int x, int y)
            {
                return State[x + y * 10];
            }
            
            public bool HasValue(int x, int y) => GetValue(x, y) != ' ';
            
            public bool CanPlace(int x, int y, int d)
            {
                // Check row
                char c = (char)(d + '0');
                for(int rx = 0; rx < 9; rx++)
                {
                    if (GetValue(rx, y) == c)
                        return false;
                }

                // Check column
                for(int cy = 0; cy < 9; cy++)
                {
                    if (GetValue(x, cy) == c)
                        return false;
                }

                // Check box
                int bxstart = (x / 3) * 3;
                int bystart = (y / 3) * 3;
                for(int bx = bxstart; bx < bxstart + 3; bx++)
                {
                    for(int by = bystart; by < bystart + 3; by++)
                    {
                        if (GetValue(bx, by) == c)
                            return false;
                    }
                }

                return true;
            }

            public SudokuProblem Place(int x, int y, int d)
            {
                return new SudokuProblem(State.Remove(x + y * 10, 1).Insert(x + y * 10, ((char)(d + '0')).ToString()));
            }

            public override AgentProblemSpace Predict(Agent agent, AgentOperation operation)
            {
                // Assumes all operations are correctly constructed.
                object[] arguments = new object[operation.Argument == null ? operation.Steps.Count : 1];
                if (operation.Argument != null)
                {
                    arguments[0] = operation.Argument;
                }
                else
                {
                    for(int i = 0; i < operation.Steps.Count; i++)
                    {
                        arguments[i] = operation.Steps[i].Executions[0].Action.Tag;
                    }
                }

                SudokuProblem currentSpace = this;

                for(int i = 0; i < arguments.Length; i++)
                {
                    (int x, int y, int d) = ((int, int, int))arguments[i];
                    currentSpace = currentSpace.Place(x, y, d);
                }

                return currentSpace;
            }
        }

        [Flags]
        public enum SudokuPossibleValue
        {
            None = 0,
            D1 = (1),
            D2 = (1 << 1),
            D3 = (1 << 2),
            D4 = (1 << 3),
            D5 = (1 << 4),
            D6 = (1 << 5),
            D7 = (1 << 6),
            D8 = (1 << 7),
            D9 = (1 << 8),
            All = 0x1FF
        }

        public class SudokuLookupGrid : AgentObject
        {
            public SudokuPossibleValue[] Values = new SudokuPossibleValue[81];

            public SudokuLookupGrid(SudokuProblem problem)
            {
                for(int i = 0; i < 81; i++)
                {
                    Values[i] = SudokuPossibleValue.All;
                }
                for(int x = 0; x < 9; x++)
                {
                    for(int y = 0; y < 9; y++)
                    {
                        char vl = problem.GetValue(x, y);
                        if(vl != ' ')
                        {
                            int d = (int)(vl - '0');
                            Remove(x, y, d);
                        }
                    }
                }
            }

            private SudokuLookupGrid()
            {

            }

            

            public override AgentObject Clone()
            {
                SudokuLookupGrid grid = new SudokuLookupGrid();
                for(int i = 0; i < 81; i++)
                {
                    grid.Values[i] = Values[i];
                }
                return grid;
            }


            public int[] GetGuesses(int x, int y)
            {
                List<int> guesses = new List<int>();
                int total = 0;
                SudokuPossibleValue value = Values[x + y * 9];
                if (value == SudokuPossibleValue.None)
                    return null;

                if(value.HasFlag(SudokuPossibleValue.D1))
                {
                    guesses.Add(1);
                    total++;
                }

                if (value.HasFlag(SudokuPossibleValue.D2))
                {
                    guesses.Add(2);
                    total++;
                }

                if (value.HasFlag(SudokuPossibleValue.D3))
                {
                    guesses.Add(3);
                    total++;
                }

                if (value.HasFlag(SudokuPossibleValue.D4))
                {
                    guesses.Add(4);
                    total++;
                }

                if (value.HasFlag(SudokuPossibleValue.D5))
                {
                    guesses.Add(5);
                    total++;
                }

                if (value.HasFlag(SudokuPossibleValue.D6))
                {
                    guesses.Add(6);
                    total++;
                }

                if (value.HasFlag(SudokuPossibleValue.D7))
                {
                    guesses.Add(7);
                    total++;
                }

                if (value.HasFlag(SudokuPossibleValue.D8))
                {
                    guesses.Add(8);
                    total++;
                }

                if (value.HasFlag(SudokuPossibleValue.D9))
                {
                    guesses.Add(9);
                    total++;
                }

                return guesses.ToArray();
            }

            public int PossibleCellsInRowFor(int d, int row)
            {
                int count = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (HasPossibleValue(i, row, d))
                    {
                        count++;
                    }
                }
                return count;
            }

            public int PossibleCellsInColFor(int d, int col)
            {
                int count = 0;
                for(int i = 0; i < 9; i++)
                {
                    if(HasPossibleValue(col, i, d))
                    {
                        count++;
                    }
                }
                return count;
            }

            public int PossibleCellsInBoxFor(int d, int x, int y)
            {
                int bxstart = (x / 3) * 3;
                int bystart = (y / 3) * 3;
                int count = 0;
                for(int bx = bxstart; bx < bxstart + 3; bx++)
                {
                    for(int by = bystart; by < bystart + 3; by++)
                    {
                        if (HasPossibleValue(x, y, d))
                            count++;
                    }
                }
                return count;
            }

            public bool HasPossibleValue(int x, int y, int d)
            {
                return HasPossibleValueAt(y * 9 + x, d);
            }

            public bool HasPossibleValueAt(int gridIndex, int d)
            {
                return ((int)Values[gridIndex] & (1 << (d - 1))) != 0;
            }

            public override void Modify(AgentOperation operation)
            {
                // Assumes all operations are correctly constructed.
                object[] arguments = new object[operation.Argument == null ? operation.Steps.Count : 1];
                if (operation.Argument != null)
                {
                    arguments[0] = operation.Argument;
                }
                else
                {
                    for (int i = 0; i < operation.Steps.Count; i++)
                    {
                        arguments[i] = operation.Steps[i].Executions[0].Action.Tag;
                    }
                }


                for (int i = 0; i < arguments.Length; i++)
                {
                    (int x, int y, int d) = ((int, int, int))arguments[i];
                    Remove(x, y, d);
                }
            }

            public void Remove(int x, int y, int d)
            {
                int index = y * 9 + x;

                // Remove all from cell
                Values[x + y * 9] = SudokuPossibleValue.None;

                // Remove from row.
                for(int i = 0; i < 9; i++)
                {
                    RemoveAt(i + y * 9, d);
                }

                // Remove from col
                for (int i = 0; i < 9; i++)
                {
                    RemoveAt(i * 9 + x, d);
                }

                // Remove from box
                int bxstart = (x / 3) * 3;
                int bystart = (y / 3) * 3;
                for(int bx = bxstart; bx < bxstart + 3; bx++)
                {
                    for(int by = bystart; by < bystart + 3; by++)
                    {
                        RemoveAt(by * 9 + bx, d);
                    }
                }
            }

            public void RemoveAt(int gridIndex, int d)
            {
                Values[gridIndex] = (SudokuPossibleValue)((int)Values[gridIndex] & ~(1 << (d - 1)));
            }
        }

        public void Solve()
        {
            // Create the agent's possible actions
            AgentAction[] possibleActions = new AgentAction[9 * 9 * 9];
            int i = 0;
            for (int x = 0; x < 9; x++)
            {
                for(int y = 0; y < 9; y++)
                {
                    for (int d = 1; d <= 9; d++)
                    {
                        possibleActions[i++] = new AgentAction($"({x}, {y}) = {d}", (agent, r) =>
                        {
                            agent.UpdateProblemUsingPrediction(AgentOperation.Single(0, (x, y, d)));
                        }, tag: (x,y,d));
                    }
                }
            }
            AgentActionSpace actionSpace = new AgentActionSpace(possibleActions);
            Agent agent = new Agent(actionSpace, null, new EliminationSolver()
            {
                SubproblemGenerator = (pspace) =>
                {
                    List<EliminationSubproblem> subproblems = new List<EliminationSubproblem>();
                    SudokuProblem problem = (SudokuProblem)pspace;
                    SudokuLookupGrid grid = new SudokuLookupGrid(problem);

                    for (int x = 0; x < 9; x++)
                    {
                        for(int y = 0; y < 9; y++)
                        {
                            if (!problem.HasValue(x, y))
                            {
                                List<AgentAction> actions = new List<AgentAction>();
                                AgentAction requiredAction = null;
                                for (int d = 1; d <= 9; d++)
                                {
                                    if (grid.HasPossibleValue(x, y, d))
                                    {
                                        AgentAction action = possibleActions[9 * 9 * x + 9 * y + (d - 1)];
                                        actions.Add(action);
                                        // Sometimes this might be the only spot that a given number can go into
                                        // so check this (row, col, box).
                                        if (grid.PossibleCellsInRowFor(d, y) == 1
                                            || grid.PossibleCellsInColFor(d, x) == 1
                                            || grid.PossibleCellsInBoxFor(d, x, y) == 1)
                                        {
                                            requiredAction = action;
                                            break; // Save some computation
                                        }
                                    }
                                }
                                if(actions.Count > 0)
                                    subproblems.Add(new EliminationSubproblem((x, y), actions.ToArray())
                                    {
                                        RequiredAction = requiredAction
                                    });
                            }
                        }
                    }
                    
                    return (subproblems, grid);
                },
                StateRefactor = (action, problemTag) =>
                {
                    SudokuLookupGrid grid = (SudokuLookupGrid)problemTag;

                    (int col, int row, int d) = ((int, int, int))action.Tag;
                    grid.Remove(col, row, d);
                },
                Refactor = (action, problem, problemTag) =>
                {
                    (int x, int y, int d) = ((int, int, int))action.Tag;
                    (int px, int py) = ((int,int))problem.Target;
                    
                    SudokuLookupGrid grid = (SudokuLookupGrid)problemTag;

                    List<AgentAction> actions = new List<AgentAction>(problem.Actions.Length);

                    // Add immediately visible actions
                    for(int i = 0; i < problem.Actions.Length; i++)
                    {
                        (int _, int _, int ad) = ((int,int,int)) problem.Actions[i].Tag;
                        if (grid.HasPossibleValue(px, py, ad))
                        {
                            actions.Add(problem.Actions[i]);

                            // Sometimes this might be the only spot that a given number can go into
                            // so check this (row, col, box).
                            if(grid.PossibleCellsInRowFor(ad, py) == 1
                                || grid.PossibleCellsInColFor(ad, px) == 1
                                || grid.PossibleCellsInBoxFor(ad, px, py) == 1)
                            {
                                problem.RequiredAction = problem.Actions[i];
                                break; // Save some computation
                            }
                        }
                    }

                    problem.Actions = actions.ToArray();
                }
            }, new AgentInfo()
            {
                Modularity = AgentModularity.Flat,
                PlanningHorizon = AgentPlanningHorizon.IndefiniteStage,
                Representation = AgentRepresentation.ExplicitStates,
                ComputationalLimits = AgentComputationalLimits.PerfectRationality,
                Learning = AgentLearningType.KnowledgeIsGiven,
                SensingUncertainty = AgentSensingUncertainty.FullyObservable,
                EffectUncertainty = AgentEffectUncertainty.Deterministic,
                Preference = AgentPreferences.AchievementGoal,
                NumberOfAgents = AgentCoordination.SingleAgent,
                Interaction = AgentInteractionTime.Offline
            });

            while(IsAlive)
            {
                Thread.Sleep(5);
                if (NextPuzzle != null)
                {
                    string puzzle = NextPuzzle;
                    NextPuzzle = null;
                    Output = puzzle;

                    SudokuProblem currentProblem = new SudokuProblem(puzzle);
                    agent.UpdateProblem(currentProblem);

                    // Solve the puzzle
                    agent.Solve();

                    // Animate the response
                    foreach(AgentOperationStep step in agent.CurrentOperation.Steps)
                    {
                        currentProblem = currentProblem.Predict(agent, new AgentOperation(step.AsNew())) as SudokuProblem;
                        Output = currentProblem.State;
                        Thread.Sleep(3);
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            IsAlive = false;
            base.OnFormClosing(e);
        }
        private void sudokuGrid1_OnGridChanged()
        {
            NextPuzzle = sudokuGrid1.Value;
        }

        private void outputUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if(Output.Length > 81)
                    sudokuGrid2.Value = Output;
            }
            catch
            {

            }
        }
    }
}
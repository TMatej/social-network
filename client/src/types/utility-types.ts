export type WithRequiredProps<TObject, TKey extends keyof TObject> = TObject & {
  [Key in TKey]-?: TObject[Key];
};

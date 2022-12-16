export type FileEntity = {
  guid: string;
  name: string;
  data: string;
};

export type User = {
  id: number;
  username: string;
  email: string;
  avatar?: FileEntity;
  roles: string[];
};

export type Profile = {
  id: number;
  name?: string;
  address: {
    city?: string;
    houseNumber?: string;
    postalCode?: string;
    region?: string;
    state?: string;
    street?: string;
  };
  createdAt: string;
  dateOfBirth?: string;
  phoneNumber?: string;
  sex?: "Male" | "Female" | "Other";
  user: User;
};

export type Photo = {
  url: string;
};

export type Gallery = {
  id: number;
  photos: Photo[];
};

export type Post = {
  id: number;
  title: string;
  content: string;
  createdAt: string;
  userId: number;
  user: User;
};

export type Paginated<TItem> = {
  page: number;
  size: number;
  items: TItem[];
};

export type Comment = {
  id: number;
  userId: number;
  user: User;
  content: string;
  createdAt: string;
};
